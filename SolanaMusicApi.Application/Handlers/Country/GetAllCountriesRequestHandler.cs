using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.CountryService;
using SolanaMusicApi.Domain.DTO.Country;

namespace SolanaMusicApi.Application.Handlers.Country;

public class GetAllCountriesRequestHandler(ICountryService countryService, IMapper mapper) : 
    IRequestHandler<GetCountriesRequest, List<CountryResponseDto>>
{
    public Task<List<CountryResponseDto>> Handle(GetCountriesRequest request, CancellationToken cancellationToken)
    {
        var countries = countryService.GetAll();
        var response = mapper.Map<List<CountryResponseDto>>(countries);
        return Task.FromResult(response);
    }
}
