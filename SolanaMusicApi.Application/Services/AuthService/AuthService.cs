using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SolanaMusicApi.Domain.DTO.Auth;
using SolanaMusicApi.Domain.DTO.Auth.OAuth;
using SolanaMusicApi.Domain.Entities.User;
using SolanaMusicApi.Domain.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SolanaMusicApi.Application.Services.AuthService;

public class AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
    IMapper mapper, IOptions<JwtTokenSettings> tokenSettings, IOptions<AuthSettings> authSettings) : IAuthService
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
        newUser.UserName = await GenerateUserName(registerDto.Email);
        var userResponse = await userManager.CreateAsync(newUser, registerDto.Password);

        if (!userResponse.Succeeded)
            AggregateErrors(userResponse.Errors);

        var roleResponse = await userManager.AddToRoleAsync(newUser, nameof(UserRoles.User));
        if (!roleResponse.Succeeded)
            AggregateErrors(roleResponse.Errors);

        var user = mapper.Map<LoginDto>(registerDto);
        return await Login(user);
    }

    public async Task<string> LoginWithExternal()
    {
        var info = await signInManager.GetExternalLoginInfoAsync();
        if (info == null)
            throw new NullReferenceException("Authentication error: external login info is null");

        var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
        if (signInResult.Succeeded)
        {
            var existingUser = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            return await GenerateToken(existingUser!);
        }

        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(email))
            throw new Exception("Could not retrieve the email from external provider");

        var user = await CheckExternalLoginUser(email);
        var addLoginResult = await userManager.AddLoginAsync(user, info);

        if (!addLoginResult.Succeeded)
            throw new Exception(AggregateErrors(addLoginResult.Errors));

        await signInManager.SignInAsync(user, false);
        return await GenerateToken(user);
    }

    public string CheckAuthProvider(string provider)
    {
        var matchedProvider = authSettings.Value.SupportedProviders
            .FirstOrDefault(p => p.Equals(provider, StringComparison.OrdinalIgnoreCase));

        if (matchedProvider == null)
            throw new Exception("Auth provider is not supported");

        return matchedProvider;
    }

    private async Task<string> GenerateToken(ApplicationUser user)
    {
        var roles = await userManager.GetRolesAsync(user);
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_tokenSettings.JwtKey);

        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id),
            new (ClaimTypes.Email, user.Email!)
        };

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

    private async Task<ApplicationUser> CheckExternalLoginUser(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user != null)
            return user;

        user = new ApplicationUser
        {
            UserName = await GenerateUserName(email),
            Email = email
        };

        var createUserResult = await userManager.CreateAsync(user);
        if (!createUserResult.Succeeded)
            throw new Exception(AggregateErrors(createUserResult.Errors));

        var roleResponse = await userManager.AddToRoleAsync(user, nameof(UserRoles.User));
        if (!roleResponse.Succeeded)
            throw new Exception(AggregateErrors(roleResponse.Errors));

        return user;
    }

    private async Task<string> GenerateUserName(string email)
    {
        var baseUserName = email.Split('@')[0];
        var userName = baseUserName;
        int suffix = 1;

        while (await userManager.FindByNameAsync(userName) != null)
        {
            userName = $"{baseUserName}{suffix}";
            suffix++;
        }

        return userName;
    }



    private string AggregateErrors(IEnumerable<IdentityError> errors)
    {
        return string.Join(" ", errors.Select(e => e.Description));
    }
}
