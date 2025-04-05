namespace SolanaMusicApi.Domain.DTO.Country;

public class CountryResponseDto : BaseResponseDto
{
    public string Name { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
}
