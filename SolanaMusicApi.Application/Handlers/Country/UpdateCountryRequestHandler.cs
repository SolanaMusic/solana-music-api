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
        var check = countryService.GetAll().Where(x => x.Id == request.Id);
        if (!check.Any())
            throw new Exception("Country does not exist");

        var country = mapper.Map<CountryEntity>(request.CountryRequestDto);
        country.Id = request.Id;
        var updated = await countryService.UpdateAsync(country);
        var response = mapper.Map<CountryResponseDto>(updated);

        return response;
    }
}
