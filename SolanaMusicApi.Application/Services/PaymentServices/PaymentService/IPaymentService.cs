using SolanaMusicApi.Domain.DTO.Payment.Stripe;
using SolanaMusicApi.Domain.DTO.Transaction;
using SolanaMusicApi.Domain.Entities.Transaction;
using Stripe.Checkout;

namespace SolanaMusicApi.Application.Services.PaymentServices.PaymentService;

public interface IPaymentService
{
    Task<Session> CreateCheckoutSessionAsync(CheckoutSessionDto checkoutSessionDto, long transactionId, long? subscriptionPlanId);
    Task<CheckoutSessionDto> GetCheckoutSessionAsync(StripeSubscriptionPaymentDto stripeSubscriptionPaymentDto,
        Transaction transaction, TransactionRequestDto transactionDto);
    
    Task RefundPaymentAsync(string paymentIntentId);
    StripeResponseDto GetStripeWebhookResponse(string json);
    Task<Session?> GetStripeSessionAsync(string paymentIntendId);
}
