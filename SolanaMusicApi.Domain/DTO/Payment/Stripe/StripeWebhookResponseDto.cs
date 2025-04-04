using Newtonsoft.Json;

namespace SolanaMusicApi.Domain.DTO.Payment.Stripe;

public class StripeWebhookResponseDto
{
    public string PaymentIntentId { get; set; } = string.Empty;
    public string Object {  get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public StripeData Data { get; set; } = null!;
}

public class StripeData
{
    public StripeObject Object { get; set; } = null!;
}

public class StripeObject
{
    [JsonProperty("payment_intent")]
    public string PaymentIntent { get; set; } = string.Empty;
    public StripeMetadata Metadata { get; set; } = null!;
}

public class StripeMetadata
{
    public string TransactionId { get; set; } = string.Empty;
    public string SubscriptionPlanId { get; set; } = string.Empty;
}