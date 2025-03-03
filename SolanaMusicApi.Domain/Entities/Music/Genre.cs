namespace SolanaMusicApi.Domain.Entities.Music;

public class Genre : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Album> Albums { get; set; } = [];
    public ICollection<TrackGenre> TrackGenres { get; set; } = [];
}
