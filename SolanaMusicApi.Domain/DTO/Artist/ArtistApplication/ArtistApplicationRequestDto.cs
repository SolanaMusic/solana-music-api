namespace SolanaMusicApi.Domain.DTO.Artist.ArtistApplication;

public class ArtistApplicationRequestDto
{
    public long UserId { get; set; }
    public string ResourceUrl { get; set; } = string.Empty;
    public string ContactLink { get; set; } = string.Empty;
}