using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests.SubscriptionPlan;
using SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionPlanService;
using SolanaMusicApi.Domain.DTO.SubscriptionPlan;

namespace SolanaMusicApi.Application.Handlers.SubscriptionPlan;

public class GetSubscriptionPlansRequestHandler(ISubscriptionPlanService subscriptionPlanService, IMapper mapper) :
    IRequestHandler<GetSubscriptionPlansRequest, List<SubscriptionPlanResponseDto>>
{
    public Task<List<SubscriptionPlanResponseDto>> Handle(GetSubscriptionPlansRequest request, CancellationToken cancellationToken)
    {
        var response = subscriptionPlanService.GetAll();
        return Task.FromResult(mapper.Map<List<SubscriptionPlanResponseDto>>(response));
    }
}
