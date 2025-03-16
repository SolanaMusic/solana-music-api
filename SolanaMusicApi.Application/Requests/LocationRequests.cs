using MediatR;
using SolanaMusicApi.Domain.DTO.General.CountryDto;

namespace SolanaMusicApi.Application.Requests;

public record CountryNameRequest : IRequest<string>;
public record CountryDetailsRequest : IRequest<CountryResponseDto>;
