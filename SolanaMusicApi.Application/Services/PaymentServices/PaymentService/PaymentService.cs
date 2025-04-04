using Microsoft.Extensions.Configuration;
using SolanaMusicApi.Application.Extensions;
using SolanaMusicApi.Application.Services.CurrencyService;
using SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionPlanService;
using SolanaMusicApi.Domain.DTO.Payment.Stripe;
using SolanaMusicApi.Domain.DTO.Transaction;
using SolanaMusicApi.Domain.Entities.Transaction;
using SolanaMusicApi.Domain.Enums.Transaction;
using Stripe;
using Stripe.Checkout;

namespace SolanaMusicApi.Application.Services.PaymentServices.PaymentService;

public class PaymentService : IPaymentService
{
    private readonly ISubscriptionPlanService _subscriptionPlanService;
    private readonly ICurrencyService _currencyService;

    public PaymentService(ISubscriptionPlanService subscriptionPlanService, ICurrencyService currencyService, IConfiguration configuration)
    {
        _subscriptionPlanService = subscriptionPlanService;
        _currencyService = currencyService;
        StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
    }

    public async Task<Session> CreateCheckoutSessionAsync(CheckoutSessionDto checkoutSessionDto, long transactionId, long? subscriptionPlanId)
    {
        var options = CreateSessionsOptions(checkoutSessionDto, transactionId, subscriptionPlanId);
        var service = new SessionService();
        var session = await service.CreateAsync(options);

        return session;
    }
    
    public async Task<CheckoutSessionDto> GetCheckoutSessionAsync(StripeSubscriptionPaymentDto stripeSubscriptionPaymentDto,
        Transaction transaction, TransactionRequestDto transactionDto)
    {
        var subscriptionPlan = await _subscriptionPlanService.GetSubscriptionPlan(stripeSubscriptionPaymentDto.SubscriptionPlanId);

        transaction.Amount = transactionDto.Amount = subscriptionPlan.SubscriptionPlanCurrencies
                                                         .FirstOrDefault(x => x.CurrencyId == transactionDto.CurrencyId)?.Price
                                                     ?? throw new Exception("Currency not found");

        var description = $"{subscriptionPlan.Type.ToString()} plan for {subscriptionPlan.DurationInMonths} months, " +
                          $"supports up to {subscriptionPlan.MaxMembers} members, " +
                          $"tokens multiplier: x{subscriptionPlan.TokensMultiplier:F2}.";

        return await MapToCheckoutSessionAsync(subscriptionPlan.Name, description, stripeSubscriptionPaymentDto, transactionDto);
    }

    public async Task RefundPaymentAsync(string paymentIntentId)
    {
        var paymentIntentService = new PaymentIntentService();
        var paymentIntent = await paymentIntentService.GetAsync(paymentIntentId);

        var refundService = new RefundService();
        var options = new RefundCreateOptions
        {
            Charge = paymentIntent.LatestChargeId,
            Amount = paymentIntent.Amount
        };

        await refundService.CreateAsync(options);
    }

    public StripeResponseDto GetStripeWebhookResponse(string json)
    {
        var webhookResponse = json.Deserialize<StripeWebhookResponseDto>()
            ?? throw new StripeException("Invalid Stripe response");

        if (webhookResponse.Object == "refund")
        {
            return new StripeResponseDto
            {
                TransactionId = 0,
                SubscriptionPlanId = 0,
                PaymentIntent = webhookResponse.PaymentIntentId,
                Status = TransactionStatus.Refunded
            };
        }

        var stripeObj = webhookResponse.Data.Object;
        var transactionId = long.TryParse(stripeObj.Metadata.TransactionId, out var parsedId) ? parsedId : 0;
        var subscriptionPlanId = long.TryParse(stripeObj.Metadata.SubscriptionPlanId, out var parsedSubId) ? parsedSubId : 0;

        return new StripeResponseDto
        {
            TransactionId = transactionId,
            SubscriptionPlanId = subscriptionPlanId,
            PaymentIntent = stripeObj.PaymentIntent,
            Status = GetResponseStatus(webhookResponse.Type)
        };
    }

    public async Task<Session?> GetStripeSessionAsync(string paymentIntendId)
    {
        var sessionService = new SessionService();
        var sessions = await sessionService.ListAsync(new SessionListOptions
        {
            PaymentIntent = paymentIntendId
        });

        return sessions.Data
            .OrderByDescending(s => s.Created)
            .FirstOrDefault(x => x.Status == "complete");
    }

    private static TransactionStatus GetResponseStatus(string status)
    {
        if (string.IsNullOrEmpty(status))
            return TransactionStatus.Unknown;

        return Enum.GetValues(typeof(TransactionStatus))
            .Cast<TransactionStatus>()
            .FirstOrDefault(ts => status.Contains(ts.ToString(), StringComparison.OrdinalIgnoreCase));
    }

    private static SessionCreateOptions CreateSessionsOptions(CheckoutSessionDto checkoutSessionDto, long transactionId, long? subscriptionPlanId)
    {
        return new SessionCreateOptions
        {
            PaymentMethodTypes = ["card"],
            LineItems = 
            [
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = checkoutSessionDto.Currency,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = checkoutSessionDto.ProductName,
                            Description = checkoutSessionDto.Description,
                            Images = [checkoutSessionDto.ImageUrl]
                        },
                        UnitAmount = (long)(checkoutSessionDto.Amount * 100)
                    },
                    Quantity = 1
                }
            ],
            Mode = "payment",
            SuccessUrl = checkoutSessionDto.SuccessUrl,
            CancelUrl = checkoutSessionDto.CancelUrl,
            Metadata = new Dictionary<string, string>
            {
                { "TransactionId", transactionId.ToString() },
                { "SubscriptionPlanId", subscriptionPlanId?.ToString() ?? "N/A" }
            }
        };
    }
    
    private async Task<CheckoutSessionDto> MapToCheckoutSessionAsync(string productName, string description, 
        StripeSubscriptionPaymentDto subscriptionPaymentDto, TransactionRequestDto transactionRequestDto)
    {
        var currency = await _currencyService.GetByIdAsync(transactionRequestDto.CurrencyId);

        return new CheckoutSessionDto
        {
            ProductName = productName,
            Description = description,
            Amount = transactionRequestDto.Amount,
            ImageUrl = subscriptionPaymentDto.ImageUrl,
            Currency = currency.Code,
            SuccessUrl = subscriptionPaymentDto.SuccessUrl,
            CancelUrl = subscriptionPaymentDto.CancelUrl
        };
    }
}
