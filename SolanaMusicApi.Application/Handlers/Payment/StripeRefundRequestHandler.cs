using MediatR;
using SolanaMusicApi.Application.Extensions;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.PaymentServices.PaymentService;
using SolanaMusicApi.Application.Services.PaymentServices.TransactionService;
using SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionService;
using SolanaMusicApi.Domain.DTO.Transaction;
using TransactionEntity = SolanaMusicApi.Domain.Entities.Transaction.Transaction;
using Stripe.Checkout;

namespace SolanaMusicApi.Application.Handlers.Payment;

public class StripeRefundRequestHandler(IPaymentService paymentService, ITransactionService transactionService, 
    ISubscriptionService subscriptionService) : IRequestHandler<StripeRefundRequest>
{
    public async Task Handle(StripeRefundRequest request, CancellationToken cancellationToken)
    {
        await transactionService.BeginTransactionAsync();
        
        try
        {
            var transaction = await transactionService.GetByIdAsync(request.RefundTransactionRequest.TransactionId);
            var session = await ValidateRefundAsync(transaction, request.RefundTransactionRequest);
            
            await paymentService.RefundPaymentAsync(transaction.PaymentIntent!);
            await subscriptionService.DeleteSubscriptionAsync(session.GetSubscriptionPlanId(), transaction.UserId);
            await transactionService.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await transactionService.RollbackTransactionAsync(ex);
            throw;
        }
    }
    
    private async Task<Session> ValidateRefundAsync(TransactionEntity transaction, RefundTransactionRequestDto refundTransactionRequest)
    {
        if (string.IsNullOrEmpty(transaction.PaymentIntent))
            throw new InvalidOperationException("Payment intent is missing");

        if (!await transactionService.CheckRefundAbilityAsync(transaction.UserId, refundTransactionRequest.TransactionId))
            throw new InvalidOperationException("Refund is not allowed");
            
        var session = await paymentService.GetStripeSessionAsync(transaction.PaymentIntent);
        if (session == null)
            throw new InvalidOperationException("Session could not be found");
        
        return session;
    }
}