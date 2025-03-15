using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionService;
using SolanaMusicApi.Domain.DTO.Subscription;
using SubscriptionEntity = SolanaMusicApi.Domain.Entities.Subscription.Subscription;

namespace SolanaMusicApi.Application.Handlers.Subscription;

public class GetSubscriptionsRequestHandler(ISubscriptionService subscriptionService, IMapper mapper) :
    IRequestHandler<GetSubscriptionsRequest, List<SubscriptionResponseDto>>
{
    public async Task<List<SubscriptionResponseDto>> Handle(GetSubscriptionsRequest request, CancellationToken cancellationToken)
    {
        var subscriptions = await subscriptionService.GetUserSubscriptions(request.UserId).ToListAsync();
        var response = mapper.Map<List<SubscriptionResponseDto>>(subscriptions);
        SetSubscriptionsActiveStatus(response, subscriptions, request.UserId);

        return response;
    }

    private void SetSubscriptionsActiveStatus(List<SubscriptionResponseDto> response, List<SubscriptionEntity> subscriptions, long userId)
    {
        foreach (var subscriptionDto in response)
        {
            var subscription = subscriptions.FirstOrDefault(s => s.Id == subscriptionDto.Id);

            if (subscription != null)
                subscriptionDto.IsActive = subscriptionService.IsSubscriptionActive(subscription, userId);
        }
    }
}
