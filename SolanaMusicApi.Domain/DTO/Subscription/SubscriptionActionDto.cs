using System.ComponentModel.DataAnnotations;

namespace SolanaMusicApi.Domain.DTO.Subscription;

public class SubscriptionActionDto
{
    [Required]
    public long Id { get; set; }
    [Required]
    public long UserId { get; set; }
}

public class SubscriptionActionsDto : SubscriptionActionDto
{
    [Required]
    public long RequestedUserId { get; set; }
}