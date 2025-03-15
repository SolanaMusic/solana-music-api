using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Application.Services.SubscriptionServices.UserSubscriptionService;
using SolanaMusicApi.Domain.Entities.Subscription;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionService;

public class SubscriptionService : BaseService<Subscription>, ISubscriptionService
{
    private readonly IUserSubscriptionService _userSubscriptionService;

    public SubscriptionService(IBaseRepository<Subscription> baseRepository, IUserSubscriptionService userSubscriptionService) : base(baseRepository) 
    {
        _userSubscriptionService = userSubscriptionService;
    }

    public IQueryable<Subscription> GetUserSubscriptions(long userId)
    {
        return GetUserSubscriptionsQuery()
            .Where(x => x.OwnerId == userId || x.UserSubscriptions.Any(x => x.UserId == userId));
    }

    public async Task<Subscription> GetSubscriptionAsync(long id, long userId)
    {
        return await GetUserSubscriptions(userId)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Subscription not found");
    }

    public async Task<Subscription> CreateSubscriptionAsync(Subscription subscription)
    {
        var added = await AddAsync(subscription);
        await DeactivateUsersSubscriptions(subscription.OwnerId);

        var userSubscription = new UserSubscription { SubscriptionId = added.Id, UserId = added.OwnerId, IsActive = true };
        await _userSubscriptionService.AddAsync(userSubscription);

        return await GetSubscriptionAsync(added.Id, added.OwnerId);
    }

    public async Task AddToSubscriptionAsync(long id, long userId, long requestedUserId)
    {
        var subscription = await GetSubscriptionAsync(id, userId);

        if (subscription.OwnerId != userId)
            throw new InvalidOperationException("Only owner can add subscription members");

        if (subscription.UserSubscriptions.Count() == subscription.SubscriptionPlan.MaxMembers)
            throw new InvalidOperationException("Max members count reached");

        if (subscription.UserSubscriptions.Any(x => x.UserId == requestedUserId))
            throw new InvalidOperationException("User allready a member of the subscription");

        var userSubscription = new UserSubscription { SubscriptionId = id, UserId = requestedUserId, IsActive = false };
        await _userSubscriptionService.AddAsync(userSubscription);
    }

    public async Task RemoveFromSubscriptionAsync(long id, long userId, long requestedUserId)
    {
        var subscription = await GetSubscriptionAsync(id, requestedUserId);

        if (subscription.OwnerId != userId && userId != requestedUserId)
            throw new InvalidOperationException("You can not remove subscription member");

        if (!subscription.UserSubscriptions.Any(x => x.UserId == requestedUserId))
            throw new InvalidOperationException("User is not a member of the subscription");

        await _userSubscriptionService.DeleteAsync(x => x.SubscriptionId == id && x.UserId == requestedUserId);
    }

    public async Task SelectSubscriptionAsync(long id, long userId)
    {
        var subscription = await GetSubscriptionAsync(id, userId);
        await DeactivateUsersSubscriptions(userId);

        await _userSubscriptionService.GetAll()
            .Where(x => x.SubscriptionId == id && x.UserId == userId)
            .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.IsActive, true));
    }

    public async Task Resubscribe(long id)
    {
        var subscription = await GetByIdAsync(id);
        await UpdateAsync(subscription);
    }

    public bool IsSubscriptionActive(Subscription subscription, long userId)
    {
        return subscription.UserSubscriptions.Any(x => x.UserId == userId && x.IsActive);
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

    private async Task DeactivateUsersSubscriptions(long userId)
    {
        var userSubscriptions = await _userSubscriptionService.GetAll()
                .Where(x => x.UserId == userId)
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.IsActive, false));
    }
}
