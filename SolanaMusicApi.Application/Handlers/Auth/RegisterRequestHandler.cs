using MediatR;
using SolanaMusicApi.Application.Requests.Auth;
using SolanaMusicApi.Application.Services.AuthService;

namespace SolanaMusicApi.Application.Handlers.Auth;

public class RegisterRequestHandler(IAuthService authService) : IRequestHandler<RegisterRequest, string>
{
    public async Task<string> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        return await authService.Register(request.RegisterDto);
    }
}
