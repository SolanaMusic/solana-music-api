using SolanaMusicApi.Domain.DTO.Dashboard.Overview;

namespace SolanaMusicApi.Application.Services.DashboardService;

public interface IDashboardService
{
    Task<DashboardOverviewResponseDto> GetOverviewAsync();
}