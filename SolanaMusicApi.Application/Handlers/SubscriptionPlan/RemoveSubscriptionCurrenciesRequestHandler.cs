using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests.SubscriptionPlan;
using SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionPlanService;

namespace SolanaMusicApi.Application.Handlers.SubscriptionPlan;

public class RemoveSubscriptionCurrenciesRequestHandler(ISubscriptionPlanService subscriptionPlanService) : IRequestHandler<RemoveSubscriptionCurrenciesRequest>
{
    public async Task Handle(RemoveSubscriptionCurrenciesRequest request, CancellationToken cancellationToken)
    {
        await subscriptionPlanService.RemoveSubscriptionCurrencies(request.Id, request.CurrencyIds);
    }
}
