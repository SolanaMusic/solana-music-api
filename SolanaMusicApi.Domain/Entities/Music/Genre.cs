namespace SolanaMusicApi.Domain.Entities.Music;

public class Genre : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public ICollection<TrackGenre> TrackGenres { get; set; } = [];
}
