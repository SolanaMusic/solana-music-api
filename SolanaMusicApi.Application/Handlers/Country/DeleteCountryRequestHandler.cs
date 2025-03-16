using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.CountryService;
using SolanaMusicApi.Domain.DTO.General.CountryDto;

namespace SolanaMusicApi.Application.Handlers.Country;

public class DeleteCountryRequestHandler(ICountryService countryService, IMapper mapper) : IRequestHandler<DeleteCountryRequest, CountryResponseDto>
{
    public async Task<CountryResponseDto> Handle(DeleteCountryRequest request, CancellationToken cancellationToken)
    {
        var response = await countryService.DeleteAsync(request.Id);
        return mapper.Map<CountryResponseDto>(response);
    }
}
