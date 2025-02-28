using SolanaMusicApi.Domain.DTO.General.CountryDto;

namespace SolanaMusicApi.Domain.DTO.User.Profile;

public class UserProfileResponseDto : BaseResponseDto
{
    public long UserId { get; set; }
    public long CountryId { get; set; }
    public string? AvatarUrl { get; set; }
    public long TokensAmount { get; set; }

    public CountryResponseDto Country { get; set; } = null!;
}
