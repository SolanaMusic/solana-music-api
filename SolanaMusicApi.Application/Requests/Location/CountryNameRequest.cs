using MediatR;

namespace SolanaMusicApi.Application.Requests.Location;

public record CountryNameRequest : IRequest<string>;
