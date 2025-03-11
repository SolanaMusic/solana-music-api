using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.Subscription;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionPlanCurrencyService;

public class SubscriptionPlanCurrencyService : BaseService<SubscriptionPlanCurrency>, ISubscriptionPlanCurrencyService
{
    public SubscriptionPlanCurrencyService(IBaseRepository<SubscriptionPlanCurrency> baseRepository) : base(baseRepository) { }
}
