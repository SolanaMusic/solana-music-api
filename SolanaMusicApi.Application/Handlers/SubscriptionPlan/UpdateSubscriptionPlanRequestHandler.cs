using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionPlanService;
using SolanaMusicApi.Domain.DTO.SubscriptionPlan;
using SubscriptionPlanEntity = SolanaMusicApi.Domain.Entities.Subscription.SubscriptionPlan;

namespace SolanaMusicApi.Application.Handlers.SubscriptionPlan;

public class UpdateSubscriptionPlanRequestHandler(ISubscriptionPlanService subscriptionPlanService, IMapper mapper) :
    IRequestHandler<UpdateSubscriptionPlanRequest, SubscriptionPlanResponseDto>
{
    public async Task<SubscriptionPlanResponseDto> Handle(UpdateSubscriptionPlanRequest request, CancellationToken cancellationToken)
    {
        var subPlan = mapper.Map<SubscriptionPlanEntity>(request.SubscriptionPlanRequestDto);
        await subscriptionPlanService.UpdateAsync(request.Id, subPlan);
        var response = await subscriptionPlanService.GetSubscriptionPlan(request.Id);

        return mapper.Map<SubscriptionPlanResponseDto>(response);
    }
}
