namespace SolanaMusicApi.Domain.DTO.Dashboard.Overview;

public class SubscriptionStatsResponseDto
{
    public List<SubscriptionStats> SubscriptionStats { get; set; } = [];
    public StatsChangeResponseDto StatsChange { get; set; } = new();
}