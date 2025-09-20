using System.ComponentModel.DataAnnotations;

namespace SolanaMusicApi.Domain.DTO.Auth.Default;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string RepeatPassword { get; set; } = string.Empty;
    
    public void Validate()
    {
        if (Password != RepeatPassword)
            throw new InvalidOperationException("Passwords do not match");
    }
}
