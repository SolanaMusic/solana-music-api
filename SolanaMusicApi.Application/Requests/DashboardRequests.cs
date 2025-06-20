using MediatR;
using SolanaMusicApi.Domain.DTO.Dashboard.Overview;

namespace SolanaMusicApi.Application.Requests;

public record GetDashboardOverviewRequest : IRequest<DashboardOverviewResponseDto>;