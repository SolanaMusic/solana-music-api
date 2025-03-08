using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Webp;
using SolanaMusicApi.Domain.Enums.File;
using SolanaMusicApi.Application.Factories.FilePathFactory;
using SolanaMusicApi.Domain.Constants;
using NAudio.Wave;

namespace SolanaMusicApi.Application.Services.FileService;

public class FileService(IFilePathFactory filePathFactory) : IFileService
{
    private readonly string _basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

    public async Task<string> SaveFileAsync(IFormFile file, FileTypes type, ImageFormats imageFormat = ImageFormats.Webp)
    {
        var extension = ValidateFileExtension(file, type);
        var fileBytes = await GetFileBytes(file, imageFormat, extension);
        var fullPath = GetFilePath(type, imageFormat, extension);

        await File.WriteAllBytesAsync(fullPath, fileBytes);
        return GetFinalPath(fullPath);
    }

    public async Task<byte[]> ConvertImageAsync(IFormFile file, ImageFormats format)
    {
        using var stream = file.OpenReadStream();
        using var image = await Image.LoadAsync(stream);
        image.Mutate(x => x.AutoOrient());

        var encoder = GetImageEncoder(format);
        using var outputStream = new MemoryStream();
        await image.SaveAsync(outputStream, encoder);

        return outputStream.ToArray();
    }

    public bool DeleteFile(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            return false;

        var fullPath = Path.Combine(_basePath, filePath);
        if (!File.Exists(fullPath))
            throw new FileNotFoundException();

        File.Delete(fullPath);
        return true;
    }

    public TimeSpan GetAudioDuration(IFormFile file)
    {
        using var stream = file.OpenReadStream();
        using var reader = Path.GetExtension(file.FileName).ToLower() == ".mp3"
            ? new Mp3FileReader(stream)
            : new WaveFileReader(stream) as WaveStream;

        return TimeSpan.FromSeconds(Math.Round(reader.TotalTime.TotalSeconds));
    }

    private string ValidateFileExtension(IFormFile file, FileTypes type)
    {
        var extension = Path.GetExtension(file.FileName).ToLower();

        if (!ExtensionConstants.AllowedExtensions.TryGetValue(type, out var allowedExtensions))
            throw new InvalidOperationException("File type is not supported");

        if (!allowedExtensions.Contains(extension))
            throw new InvalidOperationException("Extension is not supported");

        return extension;
    }

    private static IImageEncoder GetImageEncoder(ImageFormats format) =>
        format switch
        {
            ImageFormats.Png => new PngEncoder(),
            ImageFormats.Jpeg => new JpegEncoder { Quality = 85 },
            ImageFormats.Webp => new WebpEncoder { Quality = 80 },
            _ => throw new ArgumentException("Unsupported format")
        };

    private async Task<byte[]> GetFileBytes(IFormFile file, ImageFormats imageFormat, string extension)
    {
        return IsImage(extension) 
            ? await ConvertImageAsync(file, imageFormat) 
            : await ReadFileAsync(file);
    }

    private static async Task<byte[]> ReadFileAsync(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }

    private string GetFilePath(FileTypes type, ImageFormats imageFormat, string extension)
    {
        var guid = Guid.NewGuid().ToString();
        var (path, prefix) = filePathFactory.GetFilePath(type);

        if (IsImage(extension))
            extension = $".{imageFormat.ToString().ToLower()}";

        var fileName = $"{prefix}{guid}{extension}";
        var fullPath = Path.Combine(_basePath, path, fileName);

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
        return fullPath;
    }

    private string GetFinalPath(string fullPath)
    {
        if (_basePath.Contains("wwwroot", StringComparison.OrdinalIgnoreCase))
            return Path.GetRelativePath(_basePath, fullPath);

        return fullPath;
    }

    private static bool IsImage(string extension) => ExtensionConstants.ImageTypes.Contains(extension);
}
