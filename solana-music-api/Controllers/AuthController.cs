using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolanaMusicApi.Application.Requests.Auth;
using SolanaMusicApi.Domain.DTO.Auth;

namespace solana_music_api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
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

    [HttpGet("external-response")]
    public async Task<IActionResult> ExternalResponse()
    {
        var response = await mediator.Send(new ExternalResponseRequest());
        return Ok(response);
    }
}
