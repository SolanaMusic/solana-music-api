namespace SolanaMusicApi.Application.Services.LocationService;

public interface ILocationService
{
    Task<string> GetUserCountryAsync();
}
