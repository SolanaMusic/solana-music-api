using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Domain.DTO.Whitelist;

namespace solana_music_api.Controllers;

[Route("api/whitelist")]
[ApiController]
public class WhitelistController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await mediator.Send(new GetWhitelistsRequest());
        return Ok(response);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await mediator.Send(new GetWhitelistRequest(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(WhitelistRequestDto whitelistRequest)
    {
        var response = await mediator.Send(new CreateWhitelistRequest(whitelistRequest));
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(long id, WhitelistRequestDto whitelistRequest)
    {
        var response = await mediator.Send(new UpdateWhitelistRequest(id, whitelistRequest));
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await mediator.Send(new DeleteWhitelistRequest(id));
        return NoContent();
    }
}