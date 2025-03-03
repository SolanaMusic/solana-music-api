using Microsoft.AspNetCore.Http;
using SolanaMusicApi.Domain.Enums.File;

namespace SolanaMusicApi.Application.Services.FileService;

public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile file, FileTypes type, ImageFormats imageFormat = ImageFormats.Webp);
    Task<byte[]> ConvertImageAsync(IFormFile file, ImageFormats format);
    TimeSpan GetAudioDuration(IFormFile file);
    bool DeleteFile(string filePath);
}
