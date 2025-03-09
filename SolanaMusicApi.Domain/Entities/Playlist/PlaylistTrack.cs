using SolanaMusicApi.Domain.Entities.Music;

namespace SolanaMusicApi.Domain.Entities.Playlist;

public class PlaylistTrack : BaseEntity
{
    public long PlaylistId { get; set; }
    public long TrackId { get; set; }

    public Playlist Playlist { get; set; } = null!;
    public Track Track { get; set; } = null!;
}
