using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.DashboardService;
using SolanaMusicApi.Domain.DTO.Dashboard.Overview;

namespace SolanaMusicApi.Application.Handlers.Dashboard;

public class GetDashboardOverviewRequestHandler(IDashboardService dashboardService) 
    : IRequestHandler<GetDashboardOverviewRequest, DashboardOverviewResponseDto>
{
    public async Task<DashboardOverviewResponseDto> Handle(GetDashboardOverviewRequest request, CancellationToken cancellationToken) => 
        await dashboardService.GetOverviewAsync();
}