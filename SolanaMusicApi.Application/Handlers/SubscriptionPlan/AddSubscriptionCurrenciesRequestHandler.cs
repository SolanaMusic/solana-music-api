using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionPlanService;
using SolanaMusicApi.Domain.Entities.Subscription;

namespace SolanaMusicApi.Application.Handlers.SubscriptionPlan;

public class AddSubscriptionCurrenciesRequestHandler(ISubscriptionPlanService subscriptionPlanService, IMapper mapper)
    : IRequestHandler<AddSubscriptionCurrenciesRequest>
{
    public async Task Handle(AddSubscriptionCurrenciesRequest request, CancellationToken cancellationToken)
    {
        var currencies = mapper.Map<List<SubscriptionPlanCurrency>>(request.AddSubscriptionCurrenciesDto.SubscriptionPlanDtos);
        await subscriptionPlanService.AddSubscriptionCurrencies(request.AddSubscriptionCurrenciesDto.Id, currencies);
    }
}
