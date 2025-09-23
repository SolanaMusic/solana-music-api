namespace SolanaMusicApi.Domain.DTO.Dashboard.Overview;

public class SubscriptionStatsResponseDto
{
    public List<SubscriptionStats> Stats { get; set; } = [];
    public StatsChangeResponseDto Change { get; set; } = new();
}