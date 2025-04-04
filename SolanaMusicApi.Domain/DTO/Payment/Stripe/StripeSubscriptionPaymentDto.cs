using System.ComponentModel.DataAnnotations;

namespace SolanaMusicApi.Domain.DTO.Payment.Stripe;

public class StripeSubscriptionPaymentDto
{
    [Required]
    public long SubscriptionPlanId { get; set; }
    [Required]
    public string ImageUrl { get; set; } = string.Empty;
    [Required]
    public string SuccessUrl { get; set; } = string.Empty;
    [Required]
    public string CancelUrl { get; set; } = string.Empty;
}
