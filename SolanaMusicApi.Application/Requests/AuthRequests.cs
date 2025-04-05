using MediatR;
using Microsoft.AspNetCore.Authentication;
using SolanaMusicApi.Domain.DTO.Auth;
using SolanaMusicApi.Domain.DTO.Auth.Default;
using SolanaMusicApi.Domain.DTO.Auth.OAuth;

namespace SolanaMusicApi.Application.Requests;

public record LoginRequest(LoginDto LoginDto) : IRequest<AuthResponseDto>;
public record RegisterRequest(RegisterDto RegisterDto) : IRequest<AuthResponseDto>;
public record ExternalAuthRequest(string Provider, string RedirectUrl) : IRequest<(string AuthProvider, AuthenticationProperties Properties)>;
public record ExternalResponseRequest : IRequest<AuthResponseDto>;
public record PhantomAuthRequest(PhantomLoginDto PhantomLoginDto) : IRequest<AuthResponseDto>;