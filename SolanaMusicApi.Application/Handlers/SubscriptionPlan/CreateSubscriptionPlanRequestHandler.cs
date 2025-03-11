using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests.SubscriptionPlan;
using SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionPlanService;
using SolanaMusicApi.Domain.DTO.SubscriptionPlan;
using SolanaMusicApi.Domain.Entities.Subscription;
using SubscriptionPlanEntity = SolanaMusicApi.Domain.Entities.Subscription.SubscriptionPlan;

namespace SolanaMusicApi.Application.Handlers.SubscriptionPlan;

public class CreateSubscriptionPlanRequestHandler(ISubscriptionPlanService subscriptionPlanService, IMapper mapper) : 
    IRequestHandler<CreateSubscriptionPlanRequest, SubscriptionPlanResponseDto>
{
    public async Task<SubscriptionPlanResponseDto> Handle(CreateSubscriptionPlanRequest request, CancellationToken cancellationToken)
    {
        var subPlan = mapper.Map<SubscriptionPlanEntity>(request.SubscriptionPlanRequestDto);
        var currency = mapper.Map<List<SubscriptionPlanCurrency>>(request.SubscriptionPlanRequestDto.Currencies);
        var response = await subscriptionPlanService.CreateSubscriptionPlanAsync(subPlan, currency);
        return mapper.Map<SubscriptionPlanResponseDto>(response);
    }
}
