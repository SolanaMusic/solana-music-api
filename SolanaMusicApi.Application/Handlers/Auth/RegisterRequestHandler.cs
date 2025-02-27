using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests.Auth;
using SolanaMusicApi.Application.Services.AuthService;
using SolanaMusicApi.Domain.DTO.Auth;

namespace SolanaMusicApi.Application.Handlers.Auth;

public class RegisterRequestHandler(IAuthService authService, IMapper mapper) : IRequestHandler<RegisterRequest, AuthResponseDto>
{
    public async Task<AuthResponseDto> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        var response = await authService.RegisterAsync(request.RegisterDto);
        return mapper.Map<AuthResponseDto>(response);
    }
}
