namespace SolanaMusicApi.Domain.Entities.Music;

public class TrackGenre : BaseEntity
{
    public long TrackId { get; set; }
    public long GenreId { get; set; }

    public Track Track { get; set; } = null!;
    public Genre Genre { get; set; } = null!;
}
