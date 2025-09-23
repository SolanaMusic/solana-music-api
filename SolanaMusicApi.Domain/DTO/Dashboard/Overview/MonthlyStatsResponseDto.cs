namespace SolanaMusicApi.Domain.DTO.Dashboard.Overview;

public class MonthlyStatsResponseDto
{
    public DateOnly Date { get; set; }
    public decimal Value { get; set; }
}