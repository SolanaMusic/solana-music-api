using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Domain.DTO.Artist;

namespace solana_music_api.Controllers;

[Route("api/artists")]
[ApiController]
public class ArtistsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(long userId)
    {
        var response = await mediator.Send(new GetArtistsRequest(userId));
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAll(long id, long userId)
    {
        var response = await mediator.Send(new GetArtistRequest(id, userId));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateArtist([FromForm]ArtistRequestDto artistRequestDto)
    {
        var response = await mediator.Send(new CreateArtistRequest(artistRequestDto));
        return Ok(response);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateArtist(long id, [FromForm]ArtistRequestDto artistRequestDto)
    {
        var response = await mediator.Send(new UpdateArtistRequest(id, artistRequestDto));
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArtist(long id)
    {
        var response = await mediator.Send(new DeleteArtistRequest(id));
        return Ok(response);
    }

    [HttpPost("subscribe-to-artist")]
    public async Task<IActionResult> SubscribeToArtist(SubscribeToArtistDto subscribeToArtistDto)
    {
        await mediator.Send(new SubscribeToArtistRequest(subscribeToArtistDto));
        return NoContent();
    }

    [HttpDelete("unsubscribe-from-artist")]
    public async Task<IActionResult> UnsubscribeToArtist([Required]long id, [Required]long userId)
    {
        await mediator.Send(new UnsubscribeFromArtistRequest(id, userId));
        return NoContent();
    }
}
