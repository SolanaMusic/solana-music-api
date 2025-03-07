using MediatR;
using SolanaMusicApi.Domain.DTO.SubscriptionPlan;

namespace SolanaMusicApi.Application.Requests.SubscriptionPlan;

public record UpdateSubscriptionPlanRequest(long Id, SubscriptionPlanRequestDto SubscriptionPlanRequestDto) : IRequest<SubscriptionPlanResponseDto>;
