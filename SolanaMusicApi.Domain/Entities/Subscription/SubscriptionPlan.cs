using SolanaMusicApi.Domain.Enums;

namespace SolanaMusicApi.Domain.Entities.Subscription;

public class SubscriptionPlan : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int DurationInMonths { get; set; }
    public SubscriptionType Type { get; set; }
    public uint MaxMembers { get; set; }
    public double TokensMultiplier { get; set; }

    public ICollection<Subscription> Subscriptions { get; set; } = [];
    public ICollection<SubscriptionPlanCurrency> SubscriptionPlanCurrencies { get; set; } = [];
}
