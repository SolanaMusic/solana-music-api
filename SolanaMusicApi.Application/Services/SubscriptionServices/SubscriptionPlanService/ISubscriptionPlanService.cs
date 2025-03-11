using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.Subscription;

namespace SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionPlanService;

public interface ISubscriptionPlanService : IBaseService<SubscriptionPlan>
{
    IQueryable<SubscriptionPlan> GetSubscriptionPlans();
    Task<SubscriptionPlan> GetSubscriptionPlan(long id);
    Task<SubscriptionPlan> CreateSubscriptionPlanAsync(SubscriptionPlan subscriptionPlan, List<SubscriptionPlanCurrency>? currencies);
    Task AddSubscriptionCurrencies(long id, List<SubscriptionPlanCurrency> subscriptionPlanCurrencies);
    Task RemoveSubscriptionCurrencies(long id, List<long> currencyIds);
}
