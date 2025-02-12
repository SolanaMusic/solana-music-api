using System.ComponentModel.DataAnnotations;

namespace SolanaMusicApi.Domain.DTO.Auth;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public int Age { get; set; }

    [Required]
    public string Country { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
