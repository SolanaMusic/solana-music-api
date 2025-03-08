using SolanaMusicApi.Domain.Entities.General;
using SolanaMusicApi.Domain.Entities.Music;
using SolanaMusicApi.Domain.Entities.User;

namespace SolanaMusicApi.Domain.Entities.Performer;

public class Artist : BaseEntity
{
    public long? UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public long CountryId { get; set; }
    public string? Bio { get; set; }
    public string? ImageUrl { get; set; }

    public ApplicationUser? User { get; set; }
    public Country Country { get; set; } = new();
    public ICollection<ArtistAlbum> ArtistAlbums { get; set; } = [];
    public ICollection<ArtistTrack> ArtistTracks { get; set; } = [];
    public ICollection<ApplicationUser> Subscribers { get; set; } = [];
}
