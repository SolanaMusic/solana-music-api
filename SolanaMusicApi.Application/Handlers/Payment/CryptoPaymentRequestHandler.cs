using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.PaymentServices.TransactionService;
using SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionService;
using SolanaMusicApi.Domain.DTO.Payment.CryptoWallet;
using SolanaMusicApi.Domain.DTO.Transaction;
using SolanaMusicApi.Domain.Enums.Transaction;

namespace SolanaMusicApi.Application.Handlers.Payment;

public class CryptoPaymentRequestHandler(ITransactionService transactionService, ISubscriptionService subscriptionService, IMapper mapper) 
    : IRequestHandler<CryptoPaymentRequest, TransactionResponseDto>
{
    public async Task<TransactionResponseDto> Handle(CryptoPaymentRequest request, CancellationToken cancellationToken)
    {
        await transactionService.BeginTransactionAsync();
        
        try
        {
            var transactionDto = MapTransaction(request.PaymentRequestDto);
            var transaction = transactionService.CreateTransaction(transactionDto);
            var added = await transactionService.AddAsync(transaction);
            await subscriptionService.ProcessSubscriptionAsync(transaction, request.PaymentRequestDto.SubscriptionPlanId);

            await transactionService.CommitTransactionAsync();
            var response = await transactionService.GetTransactionAsync(added.Id);
            
            return mapper.Map<TransactionResponseDto>(response);
        }
        catch (Exception ex)
        {
            await transactionService.RollbackTransactionAsync(ex);
            throw;
        }
    }

    private static TransactionRequestDto MapTransaction(CryptoSubscriptionPaymentRequestDto paymentRequestDto)
    {
        return new TransactionRequestDto
        {
            UserId = paymentRequestDto.UserId,
            CurrencyId = paymentRequestDto.CurrencyId,
            Amount = paymentRequestDto.Amount,
            PaymentIntent = paymentRequestDto.PaymentIntent,
            TransactionType = TransactionType.SubscriptionPurchase,
            PaymentMethod = PaymentMethod.Crypto,
            Status = paymentRequestDto.Status,
        };
    }
}