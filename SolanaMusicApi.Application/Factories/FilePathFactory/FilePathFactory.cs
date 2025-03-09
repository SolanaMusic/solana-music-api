using SolanaMusicApi.Domain.Enums.File;

namespace SolanaMusicApi.Application.Factories.FilePathFactory;

public class FilePathFactory : IFilePathFactory
{
    public (string Path, string Prefix) GetFilePath(FileTypes fileType)
    {
        return fileType switch
        {
            FileTypes.Track => ("tracks", "track-"),
            FileTypes.AlbumCover => (Path.Combine("covers", "albums"), "album-cover-"),
            FileTypes.TrackCover => (Path.Combine("covers", "tracks"), "track-cover-"),
            FileTypes.PlaylistCover => (Path.Combine("covers", "playlists"), "playlist-cover-"),
            FileTypes.ArtistImage => (Path.Combine("images", "artists"), "artist-image-"),
            FileTypes.UserImage => (Path.Combine("images", "users"), "user-image-"),
            _ => throw new KeyNotFoundException("Unknown file type")
        };
    }
}
