using MediatR;

namespace SolanaMusicApi.Application.Requests.Auth;

public record ExternalResponseRequest : IRequest<string>;
