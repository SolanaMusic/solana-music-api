using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Domain.DTO.Album;
using System.ComponentModel.DataAnnotations;

namespace solana_music_api.Controllers;

[Route("api/albums")]
[ApiController]
public class AlbumsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await mediator.Send(new GetAlbumsRequest());
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await mediator.Send(new GetAlbumRequest(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAlbum([FromForm]AlbumRequestDto albumRequestDto)
    {
        var response = await mediator.Send(new CreateAlbumRequest(albumRequestDto));
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateAlbum(long id, [FromForm]AlbumRequestDto albumRequestDto)
    {
        var response = await mediator.Send(new UpdateAlbumRequest(id, albumRequestDto));
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAlbum(long id)
    {
        var response = await mediator.Send(new DeleteAlbumRequest(id));
        return Ok(response);
    }

    [HttpPost("add-to-album")]
    public async Task<IActionResult> AddToAlbum(AddToAlbumDto addToAlbumDto)
    {
        await mediator.Send(new AddToAlbumRequest(addToAlbumDto));
        return NoContent();
    }

    [HttpPost("remove-from-album{trackId}")]
    public async Task<IActionResult> RemoveFromAlbum(long trackId)
    {
        await mediator.Send(new RemoveFromAlbumRequest(trackId));
        return NoContent();
    }
}
