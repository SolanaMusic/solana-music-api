using MediatR;
using SolanaMusicApi.Domain.DTO.SubscriptionPlan;

namespace SolanaMusicApi.Application.Requests.SubscriptionPlan;

public record GetSubscriptionPlansRequest : IRequest<List<SubscriptionPlanResponseDto>>;
