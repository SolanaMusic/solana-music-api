using MediatR;
using SolanaMusicApi.Domain.DTO.General.CountryDto;

namespace SolanaMusicApi.Application.Requests.Country;

public record UpdateCountryRequest(long Id, CountryRequestDto CountryRequestDto) : IRequest<CountryResponseDto>;
