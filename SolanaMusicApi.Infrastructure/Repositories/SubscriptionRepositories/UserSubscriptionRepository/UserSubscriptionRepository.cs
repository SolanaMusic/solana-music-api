using SolanaMusicApi.Domain.Entities.Subscription;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.SubscriptionRepositories.UserSubscriptionRepository;

public class UserSubscriptionRepository(ApplicationDbContext context)
    : BaseRepository<UserSubscription>(context), IUserSubscriptionRepository;
