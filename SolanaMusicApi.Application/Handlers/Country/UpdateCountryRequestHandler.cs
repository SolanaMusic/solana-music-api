using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests.Country;
using SolanaMusicApi.Application.Services.CountryService;
using SolanaMusicApi.Domain.DTO.General.CountryDto;
using CountryEntity = SolanaMusicApi.Domain.Entities.General.Country;

namespace SolanaMusicApi.Application.Handlers.Country;

public class UpdateCountryRequestHandler(ICountryService countryService, IMapper mapper) : IRequestHandler<UpdateCountryRequest, CountryResponseDto>
{
    public async Task<CountryResponseDto> Handle(UpdateCountryRequest request, CancellationToken cancellationToken)
    {
        var country = mapper.Map<CountryEntity>(request.CountryRequestDto);
        var response = await countryService.UpdateAsync(request.Id, country);
        return mapper.Map<CountryResponseDto>(response);
    }
}
