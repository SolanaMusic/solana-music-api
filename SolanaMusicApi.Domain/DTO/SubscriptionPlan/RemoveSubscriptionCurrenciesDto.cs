namespace SolanaMusicApi.Domain.DTO.SubscriptionPlan;

public record RemoveSubscriptionCurrenciesDto(long Id, List<long> CurrencyIds);