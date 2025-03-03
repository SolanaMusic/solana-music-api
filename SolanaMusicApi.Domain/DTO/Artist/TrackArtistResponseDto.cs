using SolanaMusicApi.Domain.DTO.General.CountryDto;

namespace SolanaMusicApi.Domain.DTO.Artist;

public class TrackArtistResponseDto : BaseResponseDto
{
    public string Name { get; set; } = string.Empty;
    public CountryResponseDto Country { get; set; } = null!;
}
