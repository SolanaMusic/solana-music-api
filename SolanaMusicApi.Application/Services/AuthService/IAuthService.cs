using SolanaMusicApi.Domain.DTO.Auth;

namespace SolanaMusicApi.Application.Services.AuthService;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
    Task<LoginResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<LoginResponseDto> LoginWithExternalAsync();
    string CheckAuthProvider(string provider);
}
