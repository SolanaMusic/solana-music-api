using SolanaMusicApi.Domain.DTO.Currency;
using SolanaMusicApi.Domain.Entities;
using SolanaMusicApi.Domain.Enums.Transaction;

namespace SolanaMusicApi.Domain.DTO.Transaction;

public class TransactionResponseDto : BaseEntity
{
    public string? PaymentIntent { get; set; }
    public decimal Amount { get; set; }
    public TransactionStatus Status { get; set; }
    public TransactionType TransactionType { get; set; }
    public PaymentMethod PaymentMethod { get; set; }

    public CurrencyResponseDto Currency { get; set; } = null!;
}
