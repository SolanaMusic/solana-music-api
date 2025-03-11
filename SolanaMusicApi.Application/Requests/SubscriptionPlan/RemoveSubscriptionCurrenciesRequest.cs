using MediatR;

namespace SolanaMusicApi.Application.Requests.SubscriptionPlan;

public record RemoveSubscriptionCurrenciesRequest(long Id, List<long> CurrencyIds) : IRequest;
