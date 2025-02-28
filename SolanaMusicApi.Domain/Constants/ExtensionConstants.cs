using SolanaMusicApi.Domain.Enums.File;

namespace SolanaMusicApi.Domain.Constants;

public class ExtensionConstants
{
    public static readonly List<string> ImageTypes = new()
    {
        ".jpg", ".jpeg", ".jfif", ".png", ".bmp", ".gif", ".webp"
    };

    public static readonly Dictionary<FileTypes, List<string>> AllowedExtensions = new()
    {
        { FileTypes.Track, new List<string> { ".mp3", ".wav", ".flac", "aac" } },
        { FileTypes.AlbumCover, ImageTypes },
        { FileTypes.TrackCover, ImageTypes },
        { FileTypes.ArtistImage, ImageTypes },
        { FileTypes.UserImage, ImageTypes }
    };
}
