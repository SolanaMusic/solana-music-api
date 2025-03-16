using MediatR;
using SolanaMusicApi.Domain.DTO.General.CountryDto;

namespace SolanaMusicApi.Application.Requests;

public record GetCountriesRequest : IRequest<List<CountryResponseDto>>;
public record GetCountryRequest(long Id) : IRequest<CountryResponseDto>;
public record CreateCountryRequest(CountryRequestDto CountryRequestDto) : IRequest<CountryResponseDto>;
public record UpdateCountryRequest(long Id, CountryRequestDto CountryRequestDto) : IRequest<CountryResponseDto>;
public record DeleteCountryRequest(long Id) : IRequest<CountryResponseDto>;