using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests.Country;
using SolanaMusicApi.Application.Services.CountryService;
using SolanaMusicApi.Domain.DTO.General.CountryDto;
using CountryEntity = SolanaMusicApi.Domain.Entities.General.Country;

namespace SolanaMusicApi.Application.Handlers.Country;

public class CreateCountryRequestHandler(ICountryService countryService, IMapper mapper) : IRequestHandler<CreateCountryRequest, CountryResponseDto>
{
    public async Task<CountryResponseDto> Handle(CreateCountryRequest request, CancellationToken cancellationToken)
    {
        var check = countryService.GetAll()
            .Where(x => x.Name == request.CountryRequestDto.Name || x.CountryCode == request.CountryRequestDto.CountryCode);

        if (check.Any())
            throw new Exception("Country allready exists");

        var country = mapper.Map<CountryEntity>(request.CountryRequestDto);
        var added = await countryService.AddAsync(country);
        var response = mapper.Map<CountryResponseDto>(added);

        return response;
    }
}
