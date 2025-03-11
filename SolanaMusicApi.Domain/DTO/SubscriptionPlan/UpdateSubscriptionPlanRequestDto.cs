using SolanaMusicApi.Domain.Enums;

namespace SolanaMusicApi.Domain.DTO.SubscriptionPlan;

public class UpdateSubscriptionPlanRequestDto
{
    public string Name { get; set; } = string.Empty;
    public int DurationInMonths { get; set; }
    public SubscriptionType Type { get; set; }
    public uint MaxMembers { get; set; }
    public double TokensMultiplier { get; set; }
}
