using SolanaMusicApi.Domain.Entities.Transaction;

namespace SolanaMusicApi.Domain.Entities.Subscription;

public class SubscriptionPlanCurrency : BaseEntity
{
    public long CurrencyId { get; set; }
    public long SubscriptionPlanId { get; set; }
    public decimal Price { get; set; }

    public Currency Currency { get; set; } = null!;
    public SubscriptionPlan SubscriptionPlan { get; set; } = null!;
}
