namespace SolanaMusicApi.Domain.DTO.Subscription;

public class SubscriptionRequestDto
{
    public long OwnerId { get; set; }
    public long SubscriptionPlanId { get; set; }
}
