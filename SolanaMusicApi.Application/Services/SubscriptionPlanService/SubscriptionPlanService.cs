using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.Subscription;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.SubscriptionPlanService;

public class SubscriptionPlanService : BaseService<SubscriptionPlan>, ISubscriptionPlanService
{
    public SubscriptionPlanService(IBaseRepository<SubscriptionPlan> baseRepository) : base(baseRepository) { }
}
