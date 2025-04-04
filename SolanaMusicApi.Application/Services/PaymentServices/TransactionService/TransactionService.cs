using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionService;
using SolanaMusicApi.Domain.DTO.Transaction;
using SolanaMusicApi.Domain.Entities.Transaction;
using SolanaMusicApi.Domain.Enums.Transaction;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.PaymentServices.TransactionService;

public class TransactionService(IBaseRepository<Transaction> baseRepository, ISubscriptionService subscriptionService) 
    : BaseService<Transaction>(baseRepository), ITransactionService
{
    public IQueryable<Transaction> GetUserTransactions(long userId) => 
        GetTransactionsQuery().Where(x => x.UserId == userId);

    public async Task<Transaction> GetTransactionAsync(long id)
    {
        return await GetTransactionsQuery()
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Transaction not found");
    }

    public Transaction CreateTransactionAsync(TransactionRequestDto transactionDto)
    {
        return new Transaction
        {
            UserId = transactionDto.UserId,
            CurrencyId = transactionDto.CurrencyId,
            Amount = transactionDto.Amount,
            Status = TransactionStatus.Pending,
            TransactionType = transactionDto.TransactionType,
            PaymentMethod = transactionDto.PaymentMethod
        };
    }
    
    public async Task UpdateTransactionAsync(long id, TransactionStatus status, string? paymentIntentId, long? subscriptionPlanId)
    {
        await BeginTransactionAsync();

        try
        {
            var transaction = await TryGetTransactionAsync(id, status, paymentIntentId);

            var originalStatus = transaction.Status;
            var originalPaymentIntent = transaction.PaymentIntent;
            transaction.Status = status;

            if (string.IsNullOrEmpty(transaction.PaymentIntent) && !string.IsNullOrEmpty(paymentIntentId))
                transaction.PaymentIntent = paymentIntentId;
            
            if (subscriptionPlanId.HasValue)
                await subscriptionService.ProcessSubscriptionAsync(transaction, status, subscriptionPlanId.Value);

            if (transaction.Status != originalStatus || transaction.PaymentIntent != originalPaymentIntent)
                await UpdateAsync(transaction);

            await CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await RollbackTransactionAsync(ex);
        }
    }
    
    public async Task<bool> CheckRefundAbilityAsync(long userId, long transactionId)
    {
        var refunded = await GetAll()
            .Where(x => x.UserId == userId && x.Status == TransactionStatus.Refunded 
                                           && x.UpdatedDate >= DateTime.UtcNow.AddMonths(-1))
            .CountAsync();

        if (refunded > 4)
            return false;
        
        var transaction = await GetByIdAsync(transactionId);
        if (transaction.Status != TransactionStatus.Completed)
            return false;

        return transaction.UpdatedDate >= DateTime.UtcNow.AddMinutes(-20);
    }

    private IQueryable<Transaction> GetTransactionsQuery() => 
        GetAll().Include(x => x.Currency);

    private async Task<Transaction> TryGetTransactionAsync(long id, TransactionStatus status, string? paymentIntentId)
    {
        var transaction = status == TransactionStatus.Refunded
                ? await GetAsync(x => x.PaymentIntent == paymentIntentId)
                : await GetByIdAsync(id);

        if (transaction == null)
            throw new Exception("Transaction not found");

        return transaction;
    }
}
