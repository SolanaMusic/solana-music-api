namespace SolanaMusicApi.Domain.DTO.Dashboard.Overview;

public class StatsChangeResponseDto
{
    public decimal TotalValue { get; set; }
    public decimal CurrentValue { get; set; }
    public decimal PreviousValue { get; set; }
    public decimal PercentageChange { get; set; }
}