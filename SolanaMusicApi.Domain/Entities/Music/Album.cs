using SolanaMusicApi.Domain.Entities.Performer;

namespace SolanaMusicApi.Domain.Entities.Music;

public class Album : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string? Description { get; set; }

    public ICollection<ArtistAlbum> ArtistAlbums { get; set; } = [];
    public ICollection<Track> Tracks { get; set; } = [];
}
