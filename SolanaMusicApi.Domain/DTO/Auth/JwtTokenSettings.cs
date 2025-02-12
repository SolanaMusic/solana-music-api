namespace SolanaMusicApi.Domain.DTO.Auth;

public class JwtTokenSettings
{
    public string JwtIssuer { get; set; } = string.Empty;
    public string JwtAudience { get; set; } = string.Empty;
    public string JwtKey { get; set; } = string.Empty;
    public int JwtExpires { get; set; }
}
