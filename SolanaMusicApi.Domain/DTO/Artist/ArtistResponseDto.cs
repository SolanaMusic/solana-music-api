using SolanaMusicApi.Domain.DTO.General.CountryDto;
using SolanaMusicApi.Domain.DTO.User;

namespace SolanaMusicApi.Domain.DTO.Artist;

public class ArtistResponseDto : BaseResponseDto
{
    public long? UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public long CountryId { get; set; }
    public string? Bio { get; set; }
    public string? ImageUrl { get; set; }

    public UserResponseDto? User { get; set; }
    public CountryResponseDto Country { get; set; } = null!;
    public ICollection<UserResponseDto> Subscribers { get; set; } = [];
}
