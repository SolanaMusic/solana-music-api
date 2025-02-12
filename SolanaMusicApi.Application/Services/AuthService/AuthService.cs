using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SolanaMusicApi.Domain.DTO.Auth;
using SolanaMusicApi.Domain.Entities.User;
using SolanaMusicApi.Domain.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SolanaMusicApi.Application.Services.AuthService;

public class AuthService(UserManager<ApplicationUser> userManager, IMapper mapper, IOptions<JwtTokenSettings> tokenSettings) : IAuthService
{
    private readonly JwtTokenSettings _tokenSettings = tokenSettings.Value;

    public async Task<string> Login(LoginDto loginDto)
    {
        var appUser = await userManager.FindByEmailAsync(loginDto.Email);

        if (appUser == null)
            throw new NullReferenceException("User does not exist");

        if (!await userManager.CheckPasswordAsync(appUser, loginDto.Password))
            throw new Exception("Invalid password");

        return await GenerateToken(appUser);
    }

    public async Task<string> Register(RegisterDto registerDto)
    {
        var appUser = await userManager.FindByEmailAsync(registerDto.Email);

        if (appUser != null)
            throw new Exception("User is already exists");

        var newUser = mapper.Map<ApplicationUser>(registerDto);
        var response = await userManager.CreateAsync(newUser, registerDto.Password);

        if (!response.Succeeded)
        {
            var errorsMessages = response.Errors.Aggregate("", (current, error) => current + " " + error.Description);
            throw new Exception(errorsMessages);
        }

        await userManager.AddToRoleAsync(newUser, nameof(UserRoles.User));
        var user = mapper.Map<LoginDto>(registerDto);

        return await Login(user);
    }

    private async Task<string> GenerateToken(ApplicationUser user)
    {
        var roles = await userManager.GetRolesAsync(user);
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_tokenSettings.JwtKey);

        var claims = new List<Claim> { new Claim(ClaimTypes.Email, user.Email!) };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _tokenSettings.JwtIssuer,
            Audience = _tokenSettings.JwtAudience,
            Expires = DateTime.UtcNow.AddHours(_tokenSettings.JwtExpires),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
