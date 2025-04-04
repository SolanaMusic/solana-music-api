using SolanaMusicApi.Domain.Entities.Subscription;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.SubscriptionRepositories.SubscriptionPlanCurrencyRepository;

public class SubscriptionPlanCurrencyRepository(ApplicationDbContext context)
    : BaseRepository<SubscriptionPlanCurrency>(context), ISubscriptionPlanCurrencyRepository;
