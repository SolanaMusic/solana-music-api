using SolanaMusicApi.Domain.DTO.General.CountryDto;

namespace SolanaMusicApi.Domain.DTO.ArtistTrack;

public class ArtistTrackResponseDto : BaseResponseDto
{
    public string Name { get; set; } = string.Empty;
    public CountryResponseDto Country { get; set; } = null!;
}
