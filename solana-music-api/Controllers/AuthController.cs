using System.ComponentModel.DataAnnotations;
using System.Web;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Domain.DTO.Auth.Default;
using SolanaMusicApi.Domain.DTO.Auth.OAuth;

namespace solana_music_api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IMediator mediator, IConfiguration configuration) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var response = await mediator.Send(new LoginRequest(loginDto));
        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var response = await mediator.Send(new RegisterRequest(registerDto));
        return Ok(response);
    }

    [HttpGet("external-login")]
    public async Task<IActionResult> ExternalLogin([Required] string provider, [Required] string redirectUrl)
    {
        var (authProvider, properties) = await mediator.Send(new ExternalAuthRequest(provider, redirectUrl));
        return Challenge(properties, authProvider);
    }
    
    [HttpPost("solana-wallet-auth")]
    public async Task<IActionResult> SolanaWalletAuth([FromBody] SolanaWalletLoginDto solanaWalletLoginDto)
    {
        var response = await mediator.Send(new SolanaWalletAuthRequest(solanaWalletLoginDto));
        return Ok(response);
    }

    [HttpGet("external-response")]
    public async Task<IActionResult> ExternalResponse()
    {
        var response = await mediator.Send(new ExternalResponseRequest());
        
        var baseUri = configuration.GetConnectionString("Web");
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["token"] = response.Jwt;
        query["id"] = response.User.Id.ToString();
        query["username"] = response.User.UserName;
        query["role"] = response.Role;
        query["avatar"] = response.User.Profile.AvatarUrl;

        var redirectUri = $"{baseUri}?{query}";
        return Redirect(redirectUri);
    }
}
