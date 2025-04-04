using SolanaMusicApi.Domain.DTO.Payment.Stripe;
using System.ComponentModel.DataAnnotations;

namespace SolanaMusicApi.Domain.DTO.Payment;

public class SubscriptionPaymentRequestDto
{
    [Required]
    public long UserId { get; set; }
    [Required]
    public long CurrencyId { get; set; }
    public StripeSubscriptionPaymentDto? StripeSubscriptionPaymentDto { get; set; }
}
