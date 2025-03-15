using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionService;

namespace SolanaMusicApi.Application.Handlers.Subscription;

public class AddToSubscriptionRequestHandler(ISubscriptionService subscriptionService) : IRequestHandler<AddToSubscriptionRequest>
{
    public async Task Handle(AddToSubscriptionRequest request, CancellationToken cancellationToken)
    {
        var subscriptionAction = request.SubscriptionActionsDto;
        await subscriptionService.AddToSubscriptionAsync(subscriptionAction.Id, subscriptionAction.UserId, subscriptionAction.RequestedUserId);
    }
}
