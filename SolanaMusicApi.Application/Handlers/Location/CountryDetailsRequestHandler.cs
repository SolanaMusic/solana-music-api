using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.CountryService;
using SolanaMusicApi.Application.Services.LocationService;
using SolanaMusicApi.Domain.DTO.General.CountryDto;

namespace SolanaMusicApi.Application.Handlers.Location;

public class CountryDetailsRequestHandler(ILocationService locationService, ICountryService countryService, IMapper mapper) :
    IRequestHandler<CountryDetailsRequest, CountryResponseDto>
{
    public async Task<CountryResponseDto> Handle(CountryDetailsRequest request, CancellationToken cancellationToken)
    {
        var country = await locationService.GetUserCountryAsync();
        var response = await countryService.GetCountryByNameAsync(country);
        return mapper.Map<CountryResponseDto>(response);
    }
}
