using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.PaymentServices.PaymentService;
using SolanaMusicApi.Application.Services.PaymentServices.TransactionService;
using SolanaMusicApi.Domain.Enums.Transaction;

namespace SolanaMusicApi.Application.Handlers.Payment;

public class StripeWebhookRequestHandler(IPaymentService paymentService, ITransactionService transactionService) 
    : IRequestHandler<StripeWebhookRequest>
{
    public async Task Handle(StripeWebhookRequest request, CancellationToken cancellationToken)
    {
        var stripeResponse = paymentService.GetStripeWebhookResponse(request.Json);
        
        if (stripeResponse.Status != TransactionStatus.Unknown)
            await transactionService.UpdateTransactionAsync(stripeResponse.TransactionId, stripeResponse.Status, 
                stripeResponse.PaymentIntent, stripeResponse.SubscriptionPlanId);
    }
}