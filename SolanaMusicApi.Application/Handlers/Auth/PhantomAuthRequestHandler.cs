using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.AuthService;
using SolanaMusicApi.Domain.DTO.Auth;

namespace SolanaMusicApi.Application.Handlers.Auth;

public class SolanaWalletAuthRequestHandler(IAuthService authService, IMapper mapper) : 
    IRequestHandler<SolanaWalletAuthRequest, AuthResponseDto>
{
    public async Task<AuthResponseDto> Handle(SolanaWalletAuthRequest request, CancellationToken cancellationToken)
    {
        var response = await authService.LoginWithSolanaWalletAsync(request.SolanaWalletLoginDto);
        return mapper.Map<AuthResponseDto>(response);
    }
}