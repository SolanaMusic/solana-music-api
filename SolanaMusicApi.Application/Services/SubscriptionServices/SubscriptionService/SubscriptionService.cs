using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Application.Services.SubscriptionServices.UserSubscriptionService;
using SolanaMusicApi.Domain.Entities.Subscription;
using SolanaMusicApi.Domain.Entities.Transaction;
using SolanaMusicApi.Domain.Enums.Transaction;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionService;

public class SubscriptionService(IBaseRepository<Subscription> baseRepository, IUserSubscriptionService userSubscriptionService)
    : BaseService<Subscription>(baseRepository), ISubscriptionService
{
    public IQueryable<Subscription> GetUserSubscriptions(long userId)
    {
        return GetUserSubscriptionsQuery()
            .Where(x => x.OwnerId == userId || x.UserSubscriptions.Any(us => us.UserId == userId));
    }

    public async Task<Subscription> GetSubscriptionAsync(long id, long userId)
    {
        return await GetUserSubscriptions(userId)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Subscription not found");
    }

    public async Task<Subscription> CreateSubscriptionAsync(Subscription subscription)
    {
        var added = await AddAsync(subscription);
        await DeactivateUserSubscriptionsAsync(subscription.OwnerId);

        var userSubscription = new UserSubscription { SubscriptionId = added.Id, UserId = added.OwnerId, IsActive = true };
        await userSubscriptionService.AddAsync(userSubscription);

        return await GetSubscriptionAsync(added.Id, added.OwnerId);
    }

    public async Task AddToSubscriptionAsync(long id, long userId, long requestedUserId)
    {
        var subscription = await GetSubscriptionAsync(id, userId);

        if (subscription.OwnerId != userId)
            throw new InvalidOperationException("Only owner can add subscription members");

        if (subscription.UserSubscriptions.Count == subscription.SubscriptionPlan.MaxMembers)
            throw new InvalidOperationException("Max members count reached");

        if (subscription.UserSubscriptions.Any(x => x.UserId == requestedUserId))
            throw new InvalidOperationException("User already a member of the subscription");

        var userSubscription = new UserSubscription { SubscriptionId = id, UserId = requestedUserId, IsActive = false };
        await userSubscriptionService.AddAsync(userSubscription);
    }

    public async Task RemoveFromSubscriptionAsync(long id, long userId, long requestedUserId)
    {
        var subscription = await GetSubscriptionAsync(id, requestedUserId);

        if (subscription.OwnerId != userId && userId != requestedUserId)
            throw new InvalidOperationException("You can not remove subscription member");

        if (subscription.UserSubscriptions.All(x => x.UserId != requestedUserId))
            throw new InvalidOperationException("User is not a member of the subscription");

        await userSubscriptionService.DeleteAsync(x => x.SubscriptionId == id && x.UserId == requestedUserId);
    }
    
    public async Task DeleteSubscriptionAsync(long subscriptionPlanId, long userId)
    {
        var response = await GetAll()
            .Include(x => x.UserSubscriptions)
            .FirstOrDefaultAsync(x => x.SubscriptionPlanId == subscriptionPlanId && x.OwnerId == userId);

        if (response == null)
            throw new InvalidOperationException("Subscription not found");

        await userSubscriptionService.DeleteRangeAsync(response.UserSubscriptions);
        await DeleteAsync(response.Id);
    }

    public async Task SelectSubscriptionAsync(long id, long userId) 
    {
        await DeactivateUserSubscriptionsAsync(userId);

        await userSubscriptionService.GetAll()
            .Where(x => x.SubscriptionId == id && x.UserId == userId)
            .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.IsActive, true));
    }

    public async Task ResubscribeAsync(long id)
    {
        var subscription = await GetByIdAsync(id);
        await UpdateAsync(subscription);
    }

    public bool IsSubscriptionActive(Subscription subscription, long userId) => 
        subscription.UserSubscriptions.Any(x => x.UserId == userId && x.IsActive);
    
    public async Task ProcessSubscriptionAsync(Transaction transaction, long subscriptionPlanId)
    {
        var subscriptionCheck = await GetUserSubscriptions(transaction.UserId)
            .FirstOrDefaultAsync(x => x.SubscriptionPlanId == subscriptionPlanId);

        if (transaction.Status == TransactionStatus.Completed && subscriptionCheck == null)
        {
            var subscription = new Subscription { OwnerId = transaction.UserId, SubscriptionPlanId = subscriptionPlanId };
            await CreateSubscriptionAsync(subscription);
        }

        if (transaction.Status == TransactionStatus.Completed && subscriptionCheck != null)
            await ResubscribeAsync(subscriptionCheck.Id);
    }

    private IQueryable<Subscription> GetUserSubscriptionsQuery()
    {
        return GetAll()
            .Include(x => x.Owner)
                .ThenInclude(x => x.Profile)

            .Include(x => x.UserSubscriptions)
                .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Profile)

            .Include(x => x.SubscriptionPlan);
    }

    private async Task DeactivateUserSubscriptionsAsync(long userId)
    {
        await userSubscriptionService.GetAll()
            .Where(x => x.UserId == userId)
            .ExecuteUpdateAsync(s => s.SetProperty(x => x.IsActive, false));
    }
}
