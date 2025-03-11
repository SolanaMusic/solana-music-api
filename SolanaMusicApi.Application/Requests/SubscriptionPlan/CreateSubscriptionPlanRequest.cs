using MediatR;
using SolanaMusicApi.Domain.DTO.SubscriptionPlan;

namespace SolanaMusicApi.Application.Requests.SubscriptionPlan;

public record CreateSubscriptionPlanRequest(CreateSubscriptionPlanRequestDto SubscriptionPlanRequestDto) : IRequest<SubscriptionPlanResponseDto>;
