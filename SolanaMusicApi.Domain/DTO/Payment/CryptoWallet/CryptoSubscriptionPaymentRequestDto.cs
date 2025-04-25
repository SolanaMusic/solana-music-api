using System.ComponentModel.DataAnnotations;
using SolanaMusicApi.Domain.Enums.Transaction;

namespace SolanaMusicApi.Domain.DTO.Payment.CryptoWallet;

public class CryptoSubscriptionPaymentRequestDto
{
    [Required]
    public long UserId { get; set; }
    [Required]
    public long CurrencyId { get; set; }
    [Required]
    public decimal Amount { get; set; }
    [Required]
    public long SubscriptionPlanId { get; set; }
    [Required]
    public string? PaymentIntent { get; set; }
    [Required]
    public TransactionStatus Status { get; set; }
}