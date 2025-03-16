using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionPlanService;
using SolanaMusicApi.Domain.DTO.SubscriptionPlan;

namespace SolanaMusicApi.Application.Handlers.SubscriptionPlan;

public class DeleteSubscriptionPlanRequestHandler(ISubscriptionPlanService subscriptionPlanService, IMapper mapper) :
    IRequestHandler<DeleteSubscriptionPlanRequest, SubscriptionPlanResponseDto>
{
    public async Task<SubscriptionPlanResponseDto> Handle(DeleteSubscriptionPlanRequest request, CancellationToken cancellationToken)
    {
        var response = await subscriptionPlanService.DeleteAsync(request.Id);
        return mapper.Map<SubscriptionPlanResponseDto>(response);
    }
}
