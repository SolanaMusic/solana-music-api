using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests.Country;
using SolanaMusicApi.Application.Services.CountryService;
using SolanaMusicApi.Domain.DTO.General.CountryDto;

namespace SolanaMusicApi.Application.Handlers.Country;

public class GetAllCountriesRequestHandler(ICountryService countryService, IMapper mapper) : 
    IRequestHandler<GetAllCountriesRequest, List<CountryResponseDto>>
{
    public Task<List<CountryResponseDto>> Handle(GetAllCountriesRequest request, CancellationToken cancellationToken)
    {
        var countries = countryService.GetAll();
        var response = mapper.Map<List<CountryResponseDto>>(countries);
        return Task.FromResult(response);
    }
}
