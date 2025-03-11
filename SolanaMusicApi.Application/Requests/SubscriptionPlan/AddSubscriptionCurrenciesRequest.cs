using MediatR;
using SolanaMusicApi.Domain.DTO.SubscriptionPlanCurrency;
using SolanaMusicApi.Domain.Entities.Subscription;

namespace SolanaMusicApi.Application.Requests.SubscriptionPlan;

public record AddSubscriptionCurrenciesRequest(long Id, List<SubscriptionPlanCurrencyRequestDto> SubscriptionPlanCurrencyRequestDtos) : IRequest;
