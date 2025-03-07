namespace SolanaMusicApi.Domain.DTO.Currency;

public class CurrencyResponseDto : BaseResponseDto
{
    public string Code { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
}
