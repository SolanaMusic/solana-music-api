using MediatR;
using SolanaMusicApi.Domain.DTO.SubscriptionPlan;

namespace SolanaMusicApi.Application.Requests;

public record GetSubscriptionPlansRequest : IRequest<List<SubscriptionPlanResponseDto>>;
public record GetSubscriptionPlanRequest(long Id) : IRequest<SubscriptionPlanResponseDto>;
public record CreateSubscriptionPlanRequest(CreateSubscriptionPlanRequestDto SubscriptionPlanRequestDto) : IRequest<SubscriptionPlanResponseDto>;
public record AddSubscriptionCurrenciesRequest(AddSubscriptionCurrenciesDto AddSubscriptionCurrenciesDto) : IRequest;
public record RemoveSubscriptionCurrenciesRequest(RemoveSubscriptionCurrenciesDto RemoveCurrenciesDto) : IRequest;
public record UpdateSubscriptionPlanRequest(long Id, UpdateSubscriptionPlanRequestDto SubscriptionPlanRequestDto) : IRequest<SubscriptionPlanResponseDto>;
public record DeleteSubscriptionPlanRequest(long Id) : IRequest<SubscriptionPlanResponseDto>;
