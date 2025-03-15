using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.Subscription;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.SubscriptionServices.UserSubscriptionService;

public class UserSubscriptionService : BaseService<UserSubscription>, IUserSubscriptionService
{
    public UserSubscriptionService(IBaseRepository<UserSubscription> baseRepository) : base(baseRepository) { }
}
