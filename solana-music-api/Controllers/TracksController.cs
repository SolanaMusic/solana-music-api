using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolanaMusicApi.Application.Requests.Music;
using SolanaMusicApi.Domain.DTO.Track;

namespace solana_music_api.Controllers;

[Route("api/tracks")]
[ApiController]
public class TracksController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await mediator.Send(new GetTrackRequest(id));
        return Ok(response);
    }

    [HttpGet("stream/{id}")]
    public async Task<IActionResult> StreamTrack(long id)
    {
        var response = await mediator.Send(new PlayTrackRequest(id));
        return File(response, "audio/mpeg", true);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTrack(TrackRequestDto trackRequestDto)
    {
        var response = await mediator.Send(new CreateTrackRequest(trackRequestDto));
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrack(long id)
    {
        var response = await mediator.Send(new DeleteTrackRequest(id));
        return Ok(response);
    }
}
