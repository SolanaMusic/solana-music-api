using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionService;

namespace SolanaMusicApi.Application.Handlers.Subscription;

public class ResubscribeRequestHandler(ISubscriptionService subscriptionService) : IRequestHandler<ResubscribeRequest>
{
    public async Task Handle(ResubscribeRequest request, CancellationToken cancellationToken)
    {
        await subscriptionService.Resubscribe(request.Id);
    }
}
