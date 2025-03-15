using Microsoft.AspNetCore.Identity;
using SolanaMusicApi.Domain.DTO.User.Profile;
using System.Text.Json.Serialization;

namespace SolanaMusicApi.Domain.DTO.User;

public class UserResponseDto : IdentityUser<long>
{
    [JsonPropertyOrder(int.MaxValue)]
    public UserProfileResponseDto Profile { get; set; } = null!;
}
