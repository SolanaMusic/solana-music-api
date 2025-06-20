namespace SolanaMusicApi.Domain.DTO.Dashboard.Overview;

public class StatsResponseDto
{
    public List<MonthlyStatsResponseDto> Monthly { get; set; } = [];
    public StatsChangeResponseDto Change { get; set; } = new();
}