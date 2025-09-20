using Microsoft.AspNetCore.Http;

namespace SolanaMusicApi.Domain.DTO.User;

public class UpdateUserDto
{
    public string? UserName { get; set; }
    public IFormFile? Avatar { get; set; }
    public long? TokensAmount { get; set; }
}
