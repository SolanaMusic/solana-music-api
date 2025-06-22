using MediatR;
using SolanaMusicApi.Domain.DTO.Dashboard.Overview;
using SolanaMusicApi.Domain.DTO.User;

namespace SolanaMusicApi.Application.Requests;

public record GetDashboardOverviewRequest : IRequest<DashboardOverviewResponseDto>;
public record GetUsersRequest : IRequest<List<UserResponseDto>>;