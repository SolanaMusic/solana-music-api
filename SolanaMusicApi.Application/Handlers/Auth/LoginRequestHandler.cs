using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests.Auth;
using SolanaMusicApi.Application.Services.AuthService;
using SolanaMusicApi.Domain.DTO.Auth;

namespace SolanaMusicApi.Application.Handlers.Auth;

public class LoginRequestHandler(IAuthService authService, IMapper mapper) : IRequestHandler<LoginRequest, AuthResponseDto>
{
    public async Task<AuthResponseDto> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var response = await authService.LoginAsync(request.LoginDto);
        return mapper.Map<AuthResponseDto>(response);
    }
}
