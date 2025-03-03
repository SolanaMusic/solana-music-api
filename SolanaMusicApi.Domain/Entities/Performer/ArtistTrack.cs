using SolanaMusicApi.Domain.Entities.Music;

namespace SolanaMusicApi.Domain.Entities.Performer;

public class ArtistTrack
{
    public long ArtistId { get; set; }
    public long TrackId { get; set; }

    public Artist Artist { get; set; } = null!;
    public Track Track { get; set; } = null!;
}
