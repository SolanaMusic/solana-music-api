using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.Subscription;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionPlanCurrencyService;

public class SubscriptionPlanCurrencyService(IBaseRepository<SubscriptionPlanCurrency> baseRepository)
    : BaseService<SubscriptionPlanCurrency>(baseRepository), ISubscriptionPlanCurrencyService;
