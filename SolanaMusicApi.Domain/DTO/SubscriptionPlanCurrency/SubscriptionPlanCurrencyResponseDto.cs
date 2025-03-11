using SolanaMusicApi.Domain.DTO.Currency;

namespace SolanaMusicApi.Domain.DTO.SubscriptionPlanCurrency;

public class SubscriptionPlanCurrencyResponseDto : BaseResponseDto
{
    public decimal Price { get; set; }
    public CurrencyResponseDto Currency { get; set; } = null!;
}
