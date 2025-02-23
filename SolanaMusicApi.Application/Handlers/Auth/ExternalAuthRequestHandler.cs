using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using SolanaMusicApi.Application.Requests.Auth;
using SolanaMusicApi.Application.Services.AuthService;
using SolanaMusicApi.Domain.Entities.User;

namespace SolanaMusicApi.Application.Handlers.Auth;

public class ExternalAuthRequestHandler(SignInManager<ApplicationUser> signInManager, IAuthService authService) :
    IRequestHandler<ExternalAuthRequest, (string AuthProvider, AuthenticationProperties Properties)>
{
    public Task<(string AuthProvider, AuthenticationProperties Properties)> Handle(ExternalAuthRequest request, CancellationToken cancellationToken)
    {
        var authProvider = authService.CheckAuthProvider(request.Provider);
        var properties = signInManager.ConfigureExternalAuthenticationProperties(authProvider, request.RedirectUrl);
        return Task.FromResult((authProvider, properties));
    }
}
