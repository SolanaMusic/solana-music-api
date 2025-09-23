namespace SolanaMusicApi.Domain.DTO.Dashboard.Overview;

public class DashboardOverviewResponseDto
{
    public StatsResponseDto Revenue { get; set; } = new();
    public StatsResponseDto ActiveUsers { get; set; } = new();
    public StatsResponseDto NftSales { get; set; } = new();
    public SubscriptionStatsResponseDto SubscriptionStats { get; set; } = new();
}