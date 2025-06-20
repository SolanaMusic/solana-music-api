namespace SolanaMusicApi.Domain.DTO.Dashboard.Overview;

public class StatsChangeResponseDto
{
    public decimal Count { get; set; }
    public decimal Change { get; set; }
    public decimal PercentageChange { get; set; }
}