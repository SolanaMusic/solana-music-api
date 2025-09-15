namespace SolanaMusicApi.Domain.DTO.Playlist;

public class GetUserPlaylistResponseDto : BaseResponseDto
{
    public string Name { get; set; } = string.Empty;
    public string? CoverUrl { get; set; }
    public int TracksCount { get; set; }
}