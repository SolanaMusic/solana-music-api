using SolanaMusicApi.Domain.DTO.User.Profile;

namespace SolanaMusicApi.Domain.DTO.Auth;

public class RegisterDto
{
    public LoginDto LoginDto { get; set; } = null!;
    public UserProfileRequestDto UserProfileRequestDto { get; set; } = null!;
}
