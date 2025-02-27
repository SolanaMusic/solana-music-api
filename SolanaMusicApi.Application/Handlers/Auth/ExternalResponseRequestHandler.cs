using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests.Auth;
using SolanaMusicApi.Application.Services.AuthService;
using SolanaMusicApi.Domain.DTO.Auth;

namespace SolanaMusicApi.Application.Handlers.Auth;

public class ExternalResponseRequestHandler(IAuthService authService, IMapper mapper) : IRequestHandler<ExternalResponseRequest, AuthResponseDto>
{
    public async Task<AuthResponseDto> Handle(ExternalResponseRequest request, CancellationToken cancellationToken)
    {
        var response = await authService.LoginWithExternalAsync();
        return mapper.Map<AuthResponseDto>(response);
    }
}
