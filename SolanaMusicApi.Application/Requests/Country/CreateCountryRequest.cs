using MediatR;
using SolanaMusicApi.Domain.DTO.General.CountryDto;

namespace SolanaMusicApi.Application.Requests.Country;

public record CreateCountryRequest(CountryRequestDto CountryRequestDto) : IRequest<CountryResponseDto>;