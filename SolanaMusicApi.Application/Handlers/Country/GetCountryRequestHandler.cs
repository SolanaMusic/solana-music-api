using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests.Country;
using SolanaMusicApi.Application.Services.CountryService;
using SolanaMusicApi.Domain.DTO.General.CountryDto;

namespace SolanaMusicApi.Application.Handlers.Country;

public class GetCountryRequestHandler(ICountryService countryService, IMapper mapper) : IRequestHandler<GetCountryRequest, CountryResponseDto>
{
    public async Task<CountryResponseDto> Handle(GetCountryRequest request, CancellationToken cancellationToken)
    {
        var country = await countryService.GetByIdAsync(request.Id);
        var response = mapper.Map<CountryResponseDto>(country);
        return response;
    }
}
