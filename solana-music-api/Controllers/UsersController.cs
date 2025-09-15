using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolanaMusicApi.Application.Requests;

namespace solana_music_api.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUsers(long id)
    {
        var response = await mediator.Send(new GetUserRequest(id));
        return Ok(response);
    }
}