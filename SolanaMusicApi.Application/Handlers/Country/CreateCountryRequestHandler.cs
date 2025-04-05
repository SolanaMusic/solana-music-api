using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.CountryService;
using SolanaMusicApi.Domain.DTO.Country;
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
        var response = await countryService.AddAsync(country);

        return mapper.Map<CountryResponseDto>(response);
    }
}
