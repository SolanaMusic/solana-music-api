using SolanaMusicApi.Domain.Entities.Subscription;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.SubscriptionRepositories.SubscriptionRepository;

public class SubscriptionRepository(ApplicationDbContext context) : BaseRepository<Subscription>(context), ISubscriptionRepository;
