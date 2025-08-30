using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Domain.DTO.Playlist;

namespace solana_music_api.Controllers;

[Route("api/playlists")]
[ApiController]
public class PlaylistsController(IMediator mediator) : ControllerBase
{
    [HttpGet("get-user-playlists/{userId}")]
    public async Task<IActionResult> GetAll(long userId)
    {
        var response = await mediator.Send(new GetUserPlaylistsRequest(userId));
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await mediator.Send(new GetPlaylistRequest(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTrack([FromForm]CreatePlaylistRequestDto playlistRequestDto)
    {
        var response = await mediator.Send(new CreatePlaylistRequest(playlistRequestDto));
        return Ok(response);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdatePlaylist(long id, [FromForm]UpdatePlaylistRequestDto playlistRequestDto)
    {
        var response = await mediator.Send(new UpdatePlaylistRequest(id, playlistRequestDto));
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlaylist(long id)
    {
        var response = await mediator.Send(new DeletePlaylistRequest(id));
        return Ok(response);
    }

    [HttpPost("add-to-playlist")]
    public async Task<IActionResult> AddToPlaylist(AddToPlaylistDto addToPlaylistDto)
    {
        await mediator.Send(new AddToPlaylistRequest(addToPlaylistDto));
        return NoContent();
    }

    [HttpDelete("remove-from-playlist")]
    public async Task<IActionResult> RemoveFromPlaylist([Required]long playlistId, [Required]long trackId)
    {
        await mediator.Send(new RemoveFromPlaylistRequest(playlistId, trackId));
        return NoContent();
    }
}
