using SolanaMusicApi.Domain.DTO.User;

namespace SolanaMusicApi.Domain.DTO.Auth;

public class AuthResponseDto
{
    public string Jwt { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string? ArtistName { get; set; }
    
    public UserResponseDto User { get; set; } = null!;
}
