namespace SolanaMusicApi.Domain.DTO.Track;

public class GetArtistTrackResponseDto : BaseResponseDto
{
    public string Title { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public TimeSpan Duration { get; set; }
    public long PlaysCount { get; set; }
}
