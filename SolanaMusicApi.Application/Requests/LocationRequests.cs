using MediatR;
using SolanaMusicApi.Domain.DTO.Country;

namespace SolanaMusicApi.Application.Requests;

public record CountryNameRequest : IRequest<string>;
public record CountryDetailsRequest : IRequest<CountryResponseDto>;
