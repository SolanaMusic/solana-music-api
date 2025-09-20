using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Domain.DTO.User;

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
    
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateUsers(long id, UpdateUserDto updateUserDto, string? role = null)
    {
        var response = await mediator.Send(new UpdateUserRequest(id, updateUserDto));
        return Ok(response);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await mediator.Send(new DeleteUserRequest(id));
        return NoContent();
    }
}