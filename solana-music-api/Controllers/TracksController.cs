using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Domain.DTO.Track;
using SolanaMusicApi.Domain.DTO.Track.RecentlyPlayed;

namespace solana_music_api.Controllers;

[Route("api/tracks")]
[ApiController]
public class TracksController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await mediator.Send(new GetTracksRequest());
        return Ok(response);
    }
    
    [HttpGet("by-artists")]
    public async Task<IActionResult> GetByArtist([Required][FromQuery] List<long> artistIds, string? name)
    {
        var response = await mediator.Send(new GetTracksByArtistRequest(artistIds, name));
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await mediator.Send(new GetTrackRequest(id));
        return Ok(response);
    }

    [HttpGet("stream/{id}")]
    public async Task<IActionResult> StreamTrack(long id)
    {
        var response = await mediator.Send(new StreamTrackRequest(id));
        return File(response, "audio/mpeg", true);
    }

    [HttpGet("recently-played/{userId}")]
    public async Task<IActionResult> GetRecentlyPlayedTracks(long userId)
    {
        var response = await mediator.Send(new GetRecentlyPlayedRequest(userId));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTrack([FromForm]TrackRequestDto trackRequestDto)
    {
        var response = await mediator.Send(new CreateTrackRequest(trackRequestDto));
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }
    
    [HttpPost("recently-played")]
    public async Task<IActionResult> AddOrUpdateRecentlyPlayedTracks(RecentlyPlayedRequestDto recentlyPlayedRequestDto)
    {
        var response = await mediator.Send(new AddOrUpdateRecentlyPlayedRequest(recentlyPlayedRequestDto.UserId, recentlyPlayedRequestDto.TrackId));
        return Ok(response);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateTrack(long id, [FromForm]TrackRequestDto trackRequestDto)
    {
        var response = await mediator.Send(new UpdateTrackRequest(id, trackRequestDto));
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrack(long id)
    {
        var response = await mediator.Send(new DeleteTrackRequest(id));
        return Ok(response);
    }
}
