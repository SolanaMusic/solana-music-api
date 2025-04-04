using SolanaMusicApi.Domain.Entities.Subscription;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.SubscriptionRepositories.SubscriptionPlanRepository;

public class SubscriptionPlanRepository(ApplicationDbContext context)
    : BaseRepository<SubscriptionPlan>(context), ISubscriptionPlanRepository;
