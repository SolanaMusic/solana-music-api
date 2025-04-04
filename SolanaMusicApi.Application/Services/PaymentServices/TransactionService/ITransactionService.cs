using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.DTO.Transaction;
using SolanaMusicApi.Domain.Entities.Transaction;
using SolanaMusicApi.Domain.Enums.Transaction;

namespace SolanaMusicApi.Application.Services.PaymentServices.TransactionService;

public interface ITransactionService : IBaseService<Transaction>
{
    IQueryable<Transaction> GetUserTransactions(long userId);
    Task<Transaction> GetTransactionAsync(long id);
    Transaction CreateTransactionAsync(TransactionRequestDto transactionDto);
    Task UpdateTransactionAsync(long id, TransactionStatus status, string? paymentIntentId, long? subscriptionPlanId);
    Task<bool> CheckRefundAbilityAsync(long userId, long transactionId);
}
