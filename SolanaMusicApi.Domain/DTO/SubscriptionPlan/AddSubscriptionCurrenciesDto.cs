using SolanaMusicApi.Domain.DTO.SubscriptionPlanCurrency;

namespace SolanaMusicApi.Domain.DTO.SubscriptionPlan;

public record AddSubscriptionCurrenciesDto(long Id, List<SubscriptionPlanCurrencyRequestDto> SubscriptionPlanDtos);