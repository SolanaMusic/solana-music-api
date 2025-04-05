using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.AuthService;
using SolanaMusicApi.Domain.DTO.Auth;

namespace SolanaMusicApi.Application.Handlers.Auth;

public class PhantomAuthRequestHandler(IAuthService authService, IMapper mapper) : IRequestHandler<PhantomAuthRequest, AuthResponseDto>
{
    public async Task<AuthResponseDto> Handle(PhantomAuthRequest request, CancellationToken cancellationToken)
    {
        var response = await authService.LoginWithPhantomAsync(request.PhantomLoginDto);
        return mapper.Map<AuthResponseDto>(response);
    }
}