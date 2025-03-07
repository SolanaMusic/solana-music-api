using MediatR;
using SolanaMusicApi.Domain.DTO.SubscriptionPlan;

namespace SolanaMusicApi.Application.Requests.SubscriptionPlan;

public record DeleteSubscriptionPlanRequest(long Id) : IRequest<SubscriptionPlanResponseDto>;
