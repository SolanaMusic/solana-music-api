using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolanaMusicApi.Application.Requests.Music;

namespace solana_music_api.Controllers;

[Route("api/tracks")]
[ApiController]
public class TracksController(IMediator mediator) : ControllerBase
{

    [HttpGet("play/{id}")]
    public async Task<IActionResult> StreamTrack(long id)
    {
        var response = await mediator.Send(new PlayTrackRequest(id));
        return File(response, "audio/mpeg", true);
    }
}
