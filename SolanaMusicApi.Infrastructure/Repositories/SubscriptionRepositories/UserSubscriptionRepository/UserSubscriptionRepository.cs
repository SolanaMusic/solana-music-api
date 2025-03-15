using SolanaMusicApi.Application;
using SolanaMusicApi.Domain.Entities.Subscription;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.SubscriptionRepositories.UserSubscriptionRepository;

public class UserSubscriptionRepository : BaseRepository<UserSubscription>, IUserSubscriptionRepository
{
    public UserSubscriptionRepository(ApplicationDbContext context) : base(context) { }
}
