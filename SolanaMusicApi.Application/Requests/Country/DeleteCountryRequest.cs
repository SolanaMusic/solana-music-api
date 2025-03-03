using MediatR;
using SolanaMusicApi.Domain.DTO.General.CountryDto;

namespace SolanaMusicApi.Application.Requests.Country;

public record DeleteCountryRequest(long Id) : IRequest<CountryResponseDto>;
