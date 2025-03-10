using SolanaMusicApi.Domain.DTO.Genre;

namespace SolanaMusicApi.Domain.DTO.Track;

public class GetArtistTrackResponseDto : BaseResponseDto
{
    public string Title { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; }
    public long PlaysCount { get; set; }

    public ICollection<GenreResponseDto> Genres { get; set; } = null!;
}
