using SolanaMusicApi.Domain.Entities.User;

namespace SolanaMusicApi.Domain.DTO.Auth;

public class LoginResponseDto
{
    public string Jwt { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
}
