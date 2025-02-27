using MediatR;
using SolanaMusicApi.Domain.DTO.Auth;

namespace SolanaMusicApi.Application.Requests.Auth;

public record ExternalResponseRequest : IRequest<AuthResponseDto>;
