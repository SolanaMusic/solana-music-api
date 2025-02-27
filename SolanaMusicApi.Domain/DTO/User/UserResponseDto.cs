using Microsoft.AspNetCore.Identity;

namespace SolanaMusicApi.Domain.DTO.User;

public class UserResponseDto : IdentityUser<long>
{
    public long? ActiveSubscriptionId { get; set; }

    public UserProfileResponseDto Profile { get; set; } = null!;
}
