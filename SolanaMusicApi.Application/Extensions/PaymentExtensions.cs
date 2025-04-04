using Stripe.Checkout;

namespace SolanaMusicApi.Application.Extensions;

public static class PaymentExtensions
{
    public static long GetSubscriptionPlanId(this Session session)
    {
        if (!session.Metadata.TryGetValue("SubscriptionPlanId", out var subscriptionPlanIdString))
            throw new KeyNotFoundException("SubscriptionPlanId was not found");
        
        if (!long.TryParse(subscriptionPlanIdString, out var subscriptionPlanId))
            throw new FormatException("SubscriptionPlanId parsing error");
        
        return subscriptionPlanId;
    }
}