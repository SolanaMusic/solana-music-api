using SolanaMusicApi.Domain.DTO.Artist;
using SolanaMusicApi.Domain.DTO.Genre;

namespace SolanaMusicApi.Domain.DTO.Track;

public class TrackResponseDto : BaseResponseDto
{
    public string Title { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; }
    public string FileUrl { get; set; } = string.Empty;
    public uint PlaysCount { get; set; } = 0;
    public DateTime ReleaseDate { get; set; }

    public ICollection<GenreResponseDto> Genres { get; set; } = null!;
    public ICollection<TrackArtistResponseDto> Artists { get; set; } = [];
}
