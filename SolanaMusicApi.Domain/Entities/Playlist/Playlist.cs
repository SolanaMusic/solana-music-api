using SolanaMusicApi.Domain.Entities.User;

namespace SolanaMusicApi.Domain.Entities.Playlist;

public class Playlist : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public long OwnerId { get; set; }
    public string? CoverUrl { get; set; }

    public ApplicationUser Owner { get; set; } = null!;
    public ICollection<PlaylistTrack> PlaylistTracks { get; set; } = [];
}
