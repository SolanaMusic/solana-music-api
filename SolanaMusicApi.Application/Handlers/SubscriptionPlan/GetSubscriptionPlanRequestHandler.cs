using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests.SubscriptionPlan;
using SolanaMusicApi.Application.Services.SubscriptionPlanService;
using SolanaMusicApi.Domain.DTO.SubscriptionPlan;

namespace SolanaMusicApi.Application.Handlers.SubscriptionPlan;

public class GetSubscriptionPlanRequestHandler(ISubscriptionPlanService subscriptionPlanService, IMapper mapper) : 
    IRequestHandler<GetSubscriptionPlanRequest, SubscriptionPlanResponseDto>
{
    public async Task<SubscriptionPlanResponseDto> Handle(GetSubscriptionPlanRequest request, CancellationToken cancellationToken)
    {
        var response = await subscriptionPlanService.GetByIdAsync(request.Id);
        return mapper.Map<SubscriptionPlanResponseDto>(response);
    }
}
