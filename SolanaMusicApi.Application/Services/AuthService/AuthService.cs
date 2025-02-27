using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SolanaMusicApi.Application.Services.CountryService;
using SolanaMusicApi.Application.Services.UserProfileService;
using SolanaMusicApi.Domain.DTO.Auth;
using SolanaMusicApi.Domain.DTO.Auth.OAuth;
using SolanaMusicApi.Domain.Entities.User;
using SolanaMusicApi.Domain.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace SolanaMusicApi.Application.Services.AuthService;

public class AuthService(IUserProfileService userProfileService, ICountryService countryService, UserManager<ApplicationUser> userManager, 
    SignInManager<ApplicationUser> signInManager, IMapper mapper, IOptions<JwtTokenSettings> tokenSettings, 
    IOptions<AuthSettings> authSettings) : IAuthService
{
    private readonly JwtTokenSettings _tokenSettings = tokenSettings.Value;

    public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
    {
        var appUser = await userManager.Users
            .Include(u => u.Profile)
            .FirstOrDefaultAsync(u => u.Email == loginDto.Email);


        if (appUser == null)
            throw new NullReferenceException("User does not exist");

        if (!await userManager.CheckPasswordAsync(appUser, loginDto.Password))
            throw new Exception("Invalid password");

        return await GetLoginResponseAsync(appUser);
    }

    public async Task<LoginResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        var appUser = await userManager.FindByEmailAsync(registerDto.Email);

        if (appUser != null)
            throw new Exception("User is already exists");

        var newUser = mapper.Map<ApplicationUser>(registerDto);
        newUser.UserName = await GenerateUserNameAsync(registerDto.Email);
        var userResponse = await userManager.CreateAsync(newUser, registerDto.Password);

        if (!userResponse.Succeeded)
            AggregateErrors(userResponse.Errors);

        var roleResponse = await userManager.AddToRoleAsync(newUser, nameof(UserRoles.User));
        if (!roleResponse.Succeeded)
            AggregateErrors(roleResponse.Errors);

        var user = mapper.Map<LoginDto>(registerDto);
        return await LoginAsync(user);
    }

    public async Task<LoginResponseDto> LoginWithExternalAsync()
    {
        var info = await signInManager.GetExternalLoginInfoAsync();
        if (info == null)
            throw new NullReferenceException("Authentication error: external login info is null");

        var existingUser = await GetExistingUserAsync(info);
        if (existingUser != null)
            return await GetLoginResponseAsync(existingUser);

        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(email))
            throw new Exception("Could not retrieve the email from external provider");

        var user = await CheckExternalLoginUserAsync(email);
        var addLoginResult = await userManager.AddLoginAsync(user, info);

        if (!addLoginResult.Succeeded)
            throw new Exception(AggregateErrors(addLoginResult.Errors));

        await CreateUserProfileAsync(user.Id, info);
        await signInManager.SignInAsync(user, false);

        return await GetLoginResponseAsync(user);
    }

    public string CheckAuthProvider(string provider)
    {
        var matchedProvider = authSettings.Value.SupportedProviders
            .FirstOrDefault(p => p.Equals(provider, StringComparison.OrdinalIgnoreCase));

        if (matchedProvider == null)
            throw new Exception("Auth provider is not supported");

        return matchedProvider;
    }

    private async Task<string> GenerateTokenAsync(ApplicationUser user)
    {
        var roles = await userManager.GetRolesAsync(user);
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_tokenSettings.JwtKey);

        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
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

    private async Task<ApplicationUser> CheckExternalLoginUserAsync(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user != null)
            return user;

        user = new ApplicationUser
        {
            UserName = await GenerateUserNameAsync(email),
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

    private async Task<string> GenerateUserNameAsync(string email)
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

    private async Task<ApplicationUser?> GetExistingUserAsync(ExternalLoginInfo info)
    {
        var user = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
        if (user != null)
            await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

        return user;
    }

    private async Task CreateUserProfileAsync(long userId, ExternalLoginInfo info)
    {
        var country = await GetUserCountryAsync();
        var countryResponse = await countryService.GetCountryByNameAsync(country);

        var date = DateTime.UtcNow;
        var profile = new UserProfile
        {
            UserId = userId,
            AvatarUrl = GetAvatarUrl(info),
            CountryId = countryResponse?.Id ?? 1,
            TokensAmount = 0,
        };

        await userProfileService.AddAsync(profile);
    }

    private string GetAvatarUrl(ExternalLoginInfo info)
    {
        var avatar = info.Principal.Claims.FirstOrDefault(c => c.Type == "urn:google:picture")?.Value ?? string.Empty;
        return Regex.Replace(avatar, @"=s\d+", "=s300");
    }

    private async Task<string> GetUserCountryAsync()
    {
        var httpClient = new HttpClient();
        var ip = await httpClient.GetStringAsync("https://api64.ipify.org");

        var url = $"http://ip-api.com/json/{ip}";
        var response = await httpClient.GetStringAsync(url);

        using var jsonDoc = JsonDocument.Parse(response);
        return jsonDoc.RootElement.GetProperty("country").GetString() ?? "Unknown";
    }

    private async Task<LoginResponseDto> GetLoginResponseAsync(ApplicationUser user)
    {
        return new LoginResponseDto
        {
            Jwt = await GenerateTokenAsync(user),
            User = user,
        };
    }

    private string AggregateErrors(IEnumerable<IdentityError> errors) => 
        string.Join(" ", errors.Select(e => e.Description));
}
