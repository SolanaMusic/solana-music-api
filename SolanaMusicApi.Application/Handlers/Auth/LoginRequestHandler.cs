using MediatR;
using SolanaMusicApi.Application.Requests.Auth;
using SolanaMusicApi.Application.Services.AuthService;

namespace SolanaMusicApi.Application.Handlers.Auth;

public class LoginRequestHandler(IAuthService authService) : IRequestHandler<LoginRequest, string>
{
    public async Task<string> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        return await authService.Login(request.LoginDto);
    }
}
