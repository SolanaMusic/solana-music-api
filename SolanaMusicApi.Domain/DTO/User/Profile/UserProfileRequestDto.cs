namespace SolanaMusicApi.Domain.DTO.User.Profile;

public class UserProfileRequestDto
{
    public long CountryId { get; set; }
    public string AvatarUrl { get; set; } = string.Empty;
    public long TokensAmount { get; set; } = 0;
}
