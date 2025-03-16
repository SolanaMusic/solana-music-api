using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionPlanService;
using SolanaMusicApi.Domain.DTO.SubscriptionPlan;

namespace SolanaMusicApi.Application.Handlers.SubscriptionPlan;

public class GetSubscriptionPlanRequestHandler(ISubscriptionPlanService subscriptionPlanService, IMapper mapper) : 
    IRequestHandler<GetSubscriptionPlanRequest, SubscriptionPlanResponseDto>
{
    public async Task<SubscriptionPlanResponseDto> Handle(GetSubscriptionPlanRequest request, CancellationToken cancellationToken)
    {
        var response = await subscriptionPlanService.GetSubscriptionPlan(request.Id);
        return mapper.Map<SubscriptionPlanResponseDto>(response);
    }
}
