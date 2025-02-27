using SolanaMusicApi.Domain.DTO.User;

namespace SolanaMusicApi.Domain.DTO.Auth;

public class AuthResponseDto
{
    public string Jwt { get; set; } = string.Empty;
    public UserResponseDto User { get; set; } = null!;
}
