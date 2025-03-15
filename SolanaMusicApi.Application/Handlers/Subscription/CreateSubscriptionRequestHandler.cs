using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionService;
using SolanaMusicApi.Domain.DTO.Subscription;
using SubscriptionEntity = SolanaMusicApi.Domain.Entities.Subscription.Subscription;

namespace SolanaMusicApi.Application.Handlers.Subscription;

public class CreateSubscriptionRequestHandler(ISubscriptionService subscriptionService, IMapper mapper)
    : IRequestHandler<CreateSubscriptionRequest, SubscriptionResponseDto>
{
    public async Task<SubscriptionResponseDto> Handle(CreateSubscriptionRequest request, CancellationToken cancellationToken)
    {
        var subscription = mapper.Map<SubscriptionEntity>(request.SubscriptionDto);
        var added = await subscriptionService.CreateSubscriptionAsync(subscription);

        var response = mapper.Map<SubscriptionResponseDto>(added);
        response.IsActive = subscriptionService.IsSubscriptionActive(subscription, added.OwnerId);
        return response;
    }
}
