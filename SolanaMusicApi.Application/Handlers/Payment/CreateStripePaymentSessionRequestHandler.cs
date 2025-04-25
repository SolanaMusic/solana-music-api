using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.PaymentServices.PaymentService;
using SolanaMusicApi.Application.Services.PaymentServices.TransactionService;
using SolanaMusicApi.Domain.DTO.Payment;
using SolanaMusicApi.Domain.DTO.Transaction;
using SolanaMusicApi.Domain.Enums.Transaction;

namespace SolanaMusicApi.Application.Handlers.Payment;

public class CreateStripePaymentSessionRequestHandler(IPaymentService paymentService, ITransactionService transactionService) 
    : IRequestHandler<CreateStripePaymentSessionRequest, string>
{
    public async Task<string> Handle(CreateStripePaymentSessionRequest request, CancellationToken cancellationToken)
    {
        await transactionService.BeginTransactionAsync();

        try
        {
            var transactionDto = CreateTransactionRequest(request.SubscriptionPaymentRequest);
            var transaction = transactionService.CreateTransaction(transactionDto);
            
            var checkoutSession = await paymentService.GetCheckoutSessionAsync(
                request.SubscriptionPaymentRequest.StripeSubscriptionPaymentDto!, transaction, transactionDto);
            
            var addedTransaction = await transactionService.AddAsync(transaction);
            var checkoutUrl = (await paymentService.CreateCheckoutSessionAsync(checkoutSession, addedTransaction.Id, 
                request.SubscriptionPaymentRequest.StripeSubscriptionPaymentDto!.SubscriptionPlanId)).Url;
            
            await transactionService.CommitTransactionAsync();
            return checkoutUrl;
            
        }
        catch (Exception ex)
        {
            await transactionService.RollbackTransactionAsync(ex);
            throw;
        }
    }
    
    private static TransactionRequestDto CreateTransactionRequest(SubscriptionPaymentRequestDto subscriptionPaymentRequest)
    {
        return new TransactionRequestDto
        {
            UserId = subscriptionPaymentRequest.UserId,
            CurrencyId = subscriptionPaymentRequest.CurrencyId,
            PaymentMethod = PaymentMethod.BankTransfer,
            TransactionType = TransactionType.SubscriptionPurchase
        };
    }
}