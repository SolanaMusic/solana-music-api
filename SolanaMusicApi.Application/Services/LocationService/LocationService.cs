using System.Text.Json;
using Microsoft.Extensions.Options;
using SolanaMusicApi.Domain.DTO.Country;

namespace SolanaMusicApi.Application.Services.LocationService;

public class LocationService(IOptions<LocationApiOptions> options) : ILocationService
{
    public async Task<string> GetUserCountryAsync()
    {
        var httpClient = new HttpClient();
        var response = await httpClient.GetStringAsync(options.Value.GeoServiceUrl);

        using var jsonDoc = JsonDocument.Parse(response);
        return jsonDoc.RootElement.GetProperty("country").GetString() ?? "Unknown";
    }
}
