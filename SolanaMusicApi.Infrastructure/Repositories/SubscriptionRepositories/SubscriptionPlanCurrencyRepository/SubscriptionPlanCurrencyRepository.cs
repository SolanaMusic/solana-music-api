using SolanaMusicApi.Application;
using SolanaMusicApi.Domain.Entities.Subscription;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.SubscriptionRepositories.SubscriptionPlanCurrencyRepository;

public class SubscriptionPlanCurrencyRepository : BaseRepository<SubscriptionPlanCurrency>, ISubscriptionPlanCurrencyRepository
{
    public SubscriptionPlanCurrencyRepository(ApplicationDbContext context) : base(context) { }
}
