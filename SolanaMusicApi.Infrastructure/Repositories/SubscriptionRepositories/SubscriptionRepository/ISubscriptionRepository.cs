using SolanaMusicApi.Domain.Entities.Subscription;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.SubscriptionRepositories.SubscriptionRepository;

public interface ISubscriptionRepository : IBaseRepository<Subscription>;
