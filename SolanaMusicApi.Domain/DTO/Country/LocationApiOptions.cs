namespace SolanaMusicApi.Domain.DTO.Country;

public record LocationApiOptions
{
    public string GeoServiceUrl { get; init; } = string.Empty;
}