using MediatR;
using SolanaMusicApi.Application.Requests.Location;
using SolanaMusicApi.Application.Services.LocationService;

namespace SolanaMusicApi.Application.Handlers.Location;

public class CountryNameRequestHandler(ILocationService locationService) : IRequestHandler<CountryNameRequest, string>
{
    public async Task<string> Handle(CountryNameRequest request, CancellationToken cancellationToken)
    {
        return await locationService.GetUserCountryAsync();
    }
}
