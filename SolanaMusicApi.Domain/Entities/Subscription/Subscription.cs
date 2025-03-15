using SolanaMusicApi.Domain.Entities.User;

namespace SolanaMusicApi.Domain.Entities.Subscription;

public class Subscription : BaseEntity
{
    public long OwnerId { get; set; }
    public long SubscriptionPlanId { get; set; }

    public ApplicationUser Owner { get; set; } = null!;
    public SubscriptionPlan SubscriptionPlan { get; set; } = null!;
    public ICollection<UserSubscription> UserSubscriptions { get; set; } = null!;
}
