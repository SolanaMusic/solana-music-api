using SolanaMusicApi.Domain.Entities.Performer;

namespace SolanaMusicApi.Domain.Entities.Music;

public class Album : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }

    public ICollection<Artist> Artists { get; set; } = [];
    public ICollection<Track> Tracks { get; set; } = [];
    public ICollection<Genre> Genres { get; set; } = [];
}
