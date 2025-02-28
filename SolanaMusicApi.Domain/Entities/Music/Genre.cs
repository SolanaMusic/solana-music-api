namespace SolanaMusicApi.Domain.Entities.Music;

public class Genre : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Track> Tracks { get; set; } = [];
    public ICollection<Album> Albums { get; set; } = [];
}
