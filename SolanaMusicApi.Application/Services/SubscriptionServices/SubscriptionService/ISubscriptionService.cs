using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.Subscription;
using SolanaMusicApi.Domain.Entities.Transaction;
using SolanaMusicApi.Domain.Enums.Transaction;

namespace SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionService;

public interface ISubscriptionService : IBaseService<Subscription>
{
    IQueryable<Subscription> GetUserSubscriptions(long userId);
    Task<Subscription> GetSubscriptionAsync(long id, long userId);
    Task<Subscription> CreateSubscriptionAsync(Subscription subscription);
    Task AddToSubscriptionAsync(long id, long userId, long requestedUserId);
    Task RemoveFromSubscriptionAsync(long id, long userId, long requestedUserId);
    Task DeleteSubscriptionAsync(long subscriptionPlanId, long userId);
    Task SelectSubscriptionAsync(long id, long userId);
    Task ResubscribeAsync(long id);
    bool IsSubscriptionActive(Subscription subscription, long userId);
    Task ProcessSubscriptionAsync(Transaction transaction, long subscriptionPlanId);
}
