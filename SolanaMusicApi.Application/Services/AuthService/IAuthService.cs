using SolanaMusicApi.Domain.DTO.Auth;
using SolanaMusicApi.Domain.DTO.Auth.Default;
using SolanaMusicApi.Domain.DTO.Auth.OAuth;

namespace SolanaMusicApi.Application.Services.AuthService;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
    Task<LoginResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<LoginResponseDto> LoginWithExternalAsync();
    Task<LoginResponseDto> LoginWithPhantomAsync(PhantomLoginDto phantomLoginDto);
    string CheckAuthProvider(string provider);
}
