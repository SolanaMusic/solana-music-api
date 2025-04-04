using SolanaMusicApi.Domain.Enums.Transaction;

namespace SolanaMusicApi.Domain.DTO.Transaction;

public class TransactionRequestDto
{
    public long UserId { get; set; }
    public long CurrencyId { get; set; }
    public decimal Amount { get; set; }
    public TransactionType TransactionType { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}
