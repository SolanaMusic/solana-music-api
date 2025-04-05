using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SolanaMusicApi.Application.Services.CountryService;
using SolanaMusicApi.Application.Services.LocationService;
using SolanaMusicApi.Application.Services.UserServices.UserProfileService;
using SolanaMusicApi.Application.Services.UserServices.UserService;
using SolanaMusicApi.Domain.DTO.Auth;
using SolanaMusicApi.Domain.DTO.Auth.OAuth;
using SolanaMusicApi.Domain.Entities.General;
using SolanaMusicApi.Domain.Entities.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using SolanaMusicApi.Application.Extensions;
using SolanaMusicApi.Domain.DTO.Auth.Default;
using SolanaMusicApi.Domain.DTO.User.Profile;

namespace SolanaMusicApi.Application.Services.AuthService;

public class AuthService(IUserProfileService userProfileService, ICountryService countryService, UserManager<ApplicationUser> userManager, 
    ILocationService locationService, IUserService userService, SignInManager<ApplicationUser> signInManager, IMapper mapper, 
    IOptions<JwtTokenSettings> tokenSettings, IOptions<AuthSettings> authSettings) : IAuthService
{
    private readonly JwtTokenSettings _tokenSettings = tokenSettings.Value;

    public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await userService.GetUserAsync(x => x.Email == loginDto.Email);

        if (user == null)
            throw new NullReferenceException("User does not exist");

        if (!await userManager.CheckPasswordAsync(user, loginDto.Password))
            throw new AuthenticationException("Invalid password");

        return await GetLoginResponseAsync(user);
    }

    public async Task<LoginResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        var appUser = await userManager.FindByEmailAsync(registerDto.LoginDto.Email);

        if (appUser != null)
            throw new InvalidOperationException("User is already exists");

        var newUser = mapper.Map<ApplicationUser>(registerDto.LoginDto);
        newUser.UserName = await GenerateUserNameAsync(registerDto.LoginDto.Email);
        await userService.CreateUserAsync(newUser, registerDto.LoginDto.Password);

        var country = await GetUserCountry();
        await userProfileService.CreateUserProfileAsync(newUser.Id, country, registerDto.UserProfileRequestDto);

        return await LoginAsync(registerDto.LoginDto);
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
            throw new AuthenticationException("Could not retrieve the email from external provider");

        var user = await CheckExternalLoginUserAsync(email);
        var addLoginResult = await userManager.AddLoginAsync(user, info);

        if (!addLoginResult.Succeeded)
            throw new AuthenticationException(userService.AggregateErrors(addLoginResult.Errors));

        var country = await GetUserCountry();
        await userProfileService.CreateUserProfileAsync(user.Id, country, info);
        await signInManager.SignInAsync(user, false);

        return await GetLoginResponseAsync(user);
    }

    public async Task<LoginResponseDto> LoginWithPhantomAsync(PhantomLoginDto phantomLoginDto)
    {
        AuthExtensions.VerifySignature(phantomLoginDto);
        var email = AuthExtensions.GenerateEmail(phantomLoginDto.PublicKey[..6], "phantom");
        var checkUser = await userService.GetUserAsync(x => x.Email == email);
        
        if (checkUser != null)
            return await GetLoginResponseAsync(checkUser);
        
        var user = await CheckExternalLoginUserAsync(email);
        var loginInfo = new UserLoginInfo("Phantom", phantomLoginDto.PublicKey, "PhantomWallet");
        var loginResult = await userManager.AddLoginAsync(user, loginInfo);
        
        if (!loginResult.Succeeded)
            throw new AuthenticationException(userService.AggregateErrors(loginResult.Errors));
        
        var country = await GetUserCountry();
        await userProfileService.CreateUserProfileAsync(user.Id, country, new UserProfileRequestDto());
        
        return await GetLoginResponseAsync(user);
    }

    public string CheckAuthProvider(string provider)
    {
        var matchedProvider = authSettings.Value.SupportedProviders
            .FirstOrDefault(p => p.Equals(provider, StringComparison.OrdinalIgnoreCase));

        if (matchedProvider == null)
            throw new KeyNotFoundException("Auth provider is not supported");

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
        var user = await userService.GetUserAsync(x => x.Email == email);
        if (user != null)
            return user;

        user = new ApplicationUser
        {
            UserName = await GenerateUserNameAsync(email),
            Email = email
        };

        await userService.CreateUserAsync(user);
        user = await userService.GetUserAsync(x => x.Email == email);
        
        return user!;
    }

    private async Task<string> GenerateUserNameAsync(string email)
    {
        var baseUserName = email.Split('@')[0];
        var userName = baseUserName;
        var suffix = 1;

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

        return await userService.GetUserAsync(x => x.Email == info.Principal
            .FindFirstValue(ClaimTypes.Email));
    }

    private async Task<Country> GetUserCountry()
    {
        var country = await locationService.GetUserCountryAsync();
        return await countryService.GetCountryByNameAsync(country);
    }

    private async Task<LoginResponseDto> GetLoginResponseAsync(ApplicationUser user)
    {
        return new LoginResponseDto
        {
            Jwt = await GenerateTokenAsync(user),
            User = user,
        };
    }
}
