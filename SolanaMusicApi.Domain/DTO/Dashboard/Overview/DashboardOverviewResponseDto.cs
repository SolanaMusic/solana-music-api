namespace SolanaMusicApi.Domain.DTO.Dashboard.Overview;

public class DashboardOverviewResponseDto
{
    public StatsResponseDto Revenue { get; set; } = new();
    public StatsResponseDto ActiveUsers { get; set; } = new();
    public List<MonthlyStatsResponseDto> NftSales { get; set; } = [];
    public SubscriptionStatsResponseDto SubscriptionStats { get; set; } = new();
    public StatsChangeResponseDto TotalSongs { get; set; } = new();
}