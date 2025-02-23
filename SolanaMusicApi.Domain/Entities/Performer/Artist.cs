using SolanaMusicApi.Domain.Entities.Music;

namespace SolanaMusicApi.Domain.Entities.Performer;

public class Artist : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string? ImageUrl { get; set; }

    public ICollection<Album> Albums { get; set; } = [];
    public ICollection<Track> Tracks { get; set; } = [];
}
