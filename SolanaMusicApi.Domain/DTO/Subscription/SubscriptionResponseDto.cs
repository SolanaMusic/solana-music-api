using SolanaMusicApi.Domain.DTO.SubscriptionPlan;
using SolanaMusicApi.Domain.DTO.User;
namespace SolanaMusicApi.Domain.DTO.Subscription;

public class SubscriptionResponseDto : BaseResponseDto
{
    public bool IsActive { get; set; }
    public DateTime EndDate { get; set; }

    public UserResponseDto Owner { get; set; } = null!;
    public SubscriptionPlanResponseDto SubscriptionPlan { get; set; } = null!;
    public ICollection<UserResponseDto> Memebers { get; set; } = [];
}
