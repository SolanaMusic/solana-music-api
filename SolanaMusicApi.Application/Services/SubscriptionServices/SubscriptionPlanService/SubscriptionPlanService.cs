using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionPlanCurrencyService;
using SolanaMusicApi.Domain.Entities.Subscription;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionPlanService;

public class SubscriptionPlanService : BaseService<SubscriptionPlan>, ISubscriptionPlanService
{
    private readonly ISubscriptionPlanCurrencyService _subscriptionPlanCurrencyService;

    public SubscriptionPlanService(IBaseRepository<SubscriptionPlan> baseRepository, 
        ISubscriptionPlanCurrencyService subscriptionPlanCurrencyService) : base(baseRepository)
    {
        _subscriptionPlanCurrencyService = subscriptionPlanCurrencyService;
    }

    public IQueryable<SubscriptionPlan> GetSubscriptionPlans()
    {
        return GetAll()
            .Include(x => x.SubscriptionPlanCurrencies)
                .ThenInclude(x => x.Currency);
    }

    public async Task<SubscriptionPlan> GetSubscriptionPlan(long id)
    {
        return await GetSubscriptionPlans()
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Subscription plan not found");
    }

    public async Task<SubscriptionPlan> CreateSubscriptionPlanAsync(SubscriptionPlan subscriptionPlan, List<SubscriptionPlanCurrency>? currencies)
    {
        var response = await AddAsync(subscriptionPlan);

        if (currencies != null && currencies.Any())
        {
            currencies.ForEach(x => x.SubscriptionPlanId = response.Id);
            await _subscriptionPlanCurrencyService.AddRangeAsync(currencies);
        }

        return await GetSubscriptionPlan(response.Id);
    }

    public async Task AddSubscriptionCurrencies(long id, List<SubscriptionPlanCurrency> subscriptionPlanCurrencies)
    {
        var subscription = await GetSubscriptionPlan(id);

        var existingCurrencyIds = subscription.SubscriptionPlanCurrencies.Select(x => x.CurrencyId);
        var duplicateCurrencies = subscriptionPlanCurrencies
            .Where(x => existingCurrencyIds.Contains(x.CurrencyId))
            .Select(x => x.CurrencyId);

        if (duplicateCurrencies.Any())
        {
            var duplicateIds = string.Join(", ", duplicateCurrencies);
            throw new Exception($"The subscription already contains currencies: {duplicateIds}");
        }

        subscriptionPlanCurrencies.ForEach(x => x.SubscriptionPlanId = id);
        await _subscriptionPlanCurrencyService.AddRangeAsync(subscriptionPlanCurrencies);
    }

    public async Task RemoveSubscriptionCurrencies(long id, List<long> currencyIds)
    {
        var subscription = await GetSubscriptionPlan(id);
        var existingCurrencyIds = subscription.SubscriptionPlanCurrencies.Select(x => x.CurrencyId);
        var missingCurrencies = currencyIds.Except(existingCurrencyIds);

        if (missingCurrencies.Any())
        {
            var missingIds = string.Join(", ", missingCurrencies);
            throw new Exception($"The subscription does not contain currencies: {missingIds}");
        }

        var currenciesToRemove = subscription.SubscriptionPlanCurrencies
            .Where(x => currencyIds.Contains(x.CurrencyId));

        await _subscriptionPlanCurrencyService.DeleteRangeAsync(currenciesToRemove);
    }
}
