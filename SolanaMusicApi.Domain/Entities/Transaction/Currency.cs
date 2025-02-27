using SolanaMusicApi.Domain.Entities.Subscription;

namespace SolanaMusicApi.Domain.Entities.Transaction;

public class Currency : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;

    public ICollection<Transaction> Transactions { get; set; } = [];
    public ICollection<SubscriptionPlanCurrency> SubscriptionPlanCurrencies { get; set; } = [];
}
