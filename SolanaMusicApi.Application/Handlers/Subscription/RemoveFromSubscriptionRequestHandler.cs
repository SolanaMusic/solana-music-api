using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionService;

namespace SolanaMusicApi.Application.Handlers.Subscription;

public class RemoveFromSubscriptionRequestHandler(ISubscriptionService subscriptionService) : IRequestHandler<RemoveFromSubscriptionRequest>
{
    public async Task Handle(RemoveFromSubscriptionRequest request, CancellationToken cancellationToken)
    {
        var subscriptionActionDto = request.SubscriptionActionsDto;
        await subscriptionService.SelectSubscriptionAsync(subscriptionActionDto.Id, subscriptionActionDto.UserId);
    }
}
