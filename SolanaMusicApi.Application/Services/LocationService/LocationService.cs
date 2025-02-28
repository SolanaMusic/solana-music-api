using System.Text.Json;

namespace SolanaMusicApi.Application.Services.LocationService;

public class LocationService : ILocationService
{
    public async Task<string> GetUserCountryAsync()
    {
        var httpClient = new HttpClient();
        var ip = await httpClient.GetStringAsync("https://api64.ipify.org");

        var url = $"http://ip-api.com/json/{ip}";
        var response = await httpClient.GetStringAsync(url);

        using var jsonDoc = JsonDocument.Parse(response);
        return jsonDoc.RootElement.GetProperty("country").GetString() ?? "Unknown";
    }
}
