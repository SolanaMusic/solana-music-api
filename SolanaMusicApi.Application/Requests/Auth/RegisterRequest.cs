using MediatR;
using SolanaMusicApi.Domain.DTO.Auth;

namespace SolanaMusicApi.Application.Requests.Auth;

public record RegisterRequest(RegisterDto RegisterDto) : IRequest<AuthResponseDto>;