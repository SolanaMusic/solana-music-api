using SolanaMusicApi.Domain.Entities.User;
using SolanaMusicApi.Domain.Enums.Transaction;

namespace SolanaMusicApi.Domain.Entities.Transaction;

public class Transaction : BaseEntity
{
    public long UserId { get; set; }
    public string? PaymentIntent { get; set; }
    public long CurrencyId { get; set; }
    public decimal Amount { get; set; }
    public TransactionStatus Status { get; set; }
    public TransactionType TransactionType { get; set; }
    public PaymentMethod PaymentMethod { get; set; }

    public ApplicationUser User { get; set; } = null!;
    public Currency Currency { get; set; } = null!;
}
