using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionService;
using SolanaMusicApi.Domain.DTO.Subscription;

namespace SolanaMusicApi.Application.Handlers.Subscription;

public class GetSubscriptionRequestHandler(ISubscriptionService subscriptionService, IMapper mapper)
    : IRequestHandler<GetSubscriptionRequest, SubscriptionResponseDto>
{
    public async Task<SubscriptionResponseDto> Handle(GetSubscriptionRequest request, CancellationToken cancellationToken)
    {
        var subscription = await subscriptionService.GetSubscriptionAsync(request.SubscriptionActionDto.Id, request.SubscriptionActionDto.UserId);
        var response = mapper.Map<SubscriptionResponseDto>(subscription);
        response.IsActive = subscriptionService.IsSubscriptionActive(subscription, request.SubscriptionActionDto.UserId);

        return response;
    }
}
