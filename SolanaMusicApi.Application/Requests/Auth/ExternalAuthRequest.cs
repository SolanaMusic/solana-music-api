using MediatR;
using Microsoft.AspNetCore.Authentication;

namespace SolanaMusicApi.Application.Requests.Auth;

public record ExternalAuthRequest(string Provider, string RedirectUrl)
    : IRequest<(string AuthProvider, AuthenticationProperties Properties)>;

