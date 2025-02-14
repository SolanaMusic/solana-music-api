using MediatR;
using SolanaMusicApi.Application.Requests.Auth;
using SolanaMusicApi.Application.Services.AuthService;

namespace SolanaMusicApi.Application.Handlers.Auth;

public class ExternalResponseRequestHandler(IAuthService authService) : IRequestHandler<ExternalResponseRequest, string>
{
    public async Task<string> Handle(ExternalResponseRequest request, CancellationToken cancellationToken)
    {
        return await authService.LoginWithExternal();
    }
}
