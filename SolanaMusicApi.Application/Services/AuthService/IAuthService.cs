using SolanaMusicApi.Domain.DTO.Auth;

namespace SolanaMusicApi.Application.Services.AuthService;

public interface IAuthService
{
    Task<string> Login(LoginDto loginDto);
    Task<string> Register(RegisterDto registerDto);
    Task<string> LoginWithExternal();
    string CheckAuthProvider(string provider);
}
