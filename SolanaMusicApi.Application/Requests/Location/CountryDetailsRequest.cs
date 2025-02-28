using MediatR;
using SolanaMusicApi.Domain.DTO.General.CountryDto;

namespace SolanaMusicApi.Application.Requests.Location;

public record CountryDetailsRequest : IRequest<CountryResponseDto>;
