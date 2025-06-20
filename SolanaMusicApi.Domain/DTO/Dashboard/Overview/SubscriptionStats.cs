using SolanaMusicApi.Domain.Enums;

namespace SolanaMusicApi.Domain.DTO.Dashboard.Overview;

public class SubscriptionStats
{
    public SubscriptionType SubscriptionType { get; set; }
    public long Count { get; set; }
}