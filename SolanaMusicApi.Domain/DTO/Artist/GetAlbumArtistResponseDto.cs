namespace SolanaMusicApi.Domain.DTO.Artist;

public class GetAlbumArtistResponseDto : BaseResponseDto
{
    public string Name { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
}
