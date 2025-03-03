using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Domain.Entities.Playlist;

namespace SolanaMusicApi.Domain.Entities.Music;

public class Track : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public long? AlbumId { get; set; }
    public string? ImageUrl { get; set; }
    public TimeSpan Duration { get; set; }
    public string FileUrl { get; set; } = string.Empty;
    public uint PlaysCount { get; set; } = 0;
    public DateTime ReleaseDate { get; set; }

    public Album? Album { get; set; }
    public ICollection<TrackGenre> TrackGenres { get; set; } = [];
    public ICollection<ArtistTrack> ArtistTracks { get; set; } = [];
    public ICollection<PlaylistTrack> PlaylistTracks { get; set; } = [];
}
