using SolanaMusicApi.Domain.Entities.User;

namespace SolanaMusicApi.Domain.Entities.Subscription;

public class UserSubscription : BaseEntity
{
    public long UserId { get; set; }
    public long SubscriptionId { get; set; }

    public ApplicationUser User { get; set; } = null!;
    public Subscription Subscription { get; set; } = null!;
}
