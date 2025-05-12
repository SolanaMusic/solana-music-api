using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Domain.DTO.Genre;

namespace solana_music_api.Controllers;

[Route("api/genres")]
[ApiController]
public class GenresController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await mediator.Send(new GetGenresRequest());
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await mediator.Send(new GetGenreRequest(id));
        return Ok(response);
    }
    
    [HttpGet("by-name")]
    public async Task<IActionResult> GetByName(string? name)
    {
        var response = await mediator.Send(new GetGenresByNameRequest(name));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddGenre(GenreRequestDto genreRequestDto)
    {
        var response = await mediator.Send(new CreateGenreRequest(genreRequestDto));
        return Ok(response);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateGenre(long id, GenreRequestDto genreRequestDto)
    {
        var response = await mediator.Send(new UpdateGenreRequest(id, genreRequestDto));
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(long id)
    {
        var response = await mediator.Send(new DeleteGenreRequest(id));
        return Ok(response);
    }
}
