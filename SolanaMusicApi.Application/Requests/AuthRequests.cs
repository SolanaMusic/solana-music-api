using MediatR;
using Microsoft.AspNetCore.Authentication;
using SolanaMusicApi.Domain.DTO.Auth;

namespace SolanaMusicApi.Application.Requests;

public record LoginRequest(LoginDto LoginDto) : IRequest<AuthResponseDto>;
public record RegisterRequest(RegisterDto RegisterDto) : IRequest<AuthResponseDto>;
public record ExternalAuthRequest(string Provider, string RedirectUrl) : IRequest<(string AuthProvider, AuthenticationProperties Properties)>;
public record ExternalResponseRequest : IRequest<AuthResponseDto>;