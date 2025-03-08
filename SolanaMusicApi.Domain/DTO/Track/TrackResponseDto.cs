using SolanaMusicApi.Domain.DTO.ArtistTrack;
using SolanaMusicApi.Domain.DTO.Genre;

namespace SolanaMusicApi.Domain.DTO.Track;

public class TrackResponseDto : BaseResponseDto
{
    public string Title { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; }
    public string FileUrl { get; set; } = string.Empty;
    public long PlaysCount { get; set; }
    public DateTime ReleaseDate { get; set; }

    public ICollection<GenreResponseDto> Genres { get; set; } = null!;
    public ICollection<ArtistTrackResponseDto> Artists { get; set; } = [];
}
