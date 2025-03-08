using SolanaMusicApi.Domain.Entities.Music;

namespace SolanaMusicApi.Domain.Entities.Performer;

public class ArtistAlbum : BaseEntity
{
    public long ArtistId { get; set; }
    public long AlbumId { get; set; }

    public Artist Artist { get; set; } = null!;
    public Album Album { get; set; } = null!;
}
