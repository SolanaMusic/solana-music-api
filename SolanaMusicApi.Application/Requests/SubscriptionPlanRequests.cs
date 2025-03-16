using MediatR;
using SolanaMusicApi.Domain.DTO.SubscriptionPlan;
using SolanaMusicApi.Domain.DTO.SubscriptionPlanCurrency;

namespace SolanaMusicApi.Application.Requests;

public record GetSubscriptionPlansRequest : IRequest<List<SubscriptionPlanResponseDto>>;
public record GetSubscriptionPlanRequest(long Id) : IRequest<SubscriptionPlanResponseDto>;
public record CreateSubscriptionPlanRequest(CreateSubscriptionPlanRequestDto SubscriptionPlanRequestDto) : IRequest<SubscriptionPlanResponseDto>;
public record AddSubscriptionCurrenciesRequest(long Id, List<SubscriptionPlanCurrencyRequestDto> SubscriptionPlanCurrencyRequestDtos) : IRequest;
public record RemoveSubscriptionCurrenciesRequest(long Id, List<long> CurrencyIds) : IRequest;
public record UpdateSubscriptionPlanRequest(long Id, UpdateSubscriptionPlanRequestDto SubscriptionPlanRequestDto) : IRequest<SubscriptionPlanResponseDto>;
public record DeleteSubscriptionPlanRequest(long Id) : IRequest<SubscriptionPlanResponseDto>;
