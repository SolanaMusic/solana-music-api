using SolanaMusicApi.Application;
using SolanaMusicApi.Domain.Entities.Subscription;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.SubscriptionRepositories.SubscriptionRepository;

public class SubscriptionRepository : BaseRepository<Subscription>, ISubscriptionRepository
{
    public SubscriptionRepository(ApplicationDbContext context) : base(context) { }
}
