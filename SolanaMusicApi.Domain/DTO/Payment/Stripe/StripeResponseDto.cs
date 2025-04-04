using SolanaMusicApi.Domain.Enums.Transaction;

namespace SolanaMusicApi.Domain.DTO.Payment.Stripe;

public class StripeResponseDto
{
    public long TransactionId { get; set; }
    public long SubscriptionPlanId { get; set; }
    public string? PaymentIntent { get; set; }
    public TransactionStatus Status { get; set; }
}
