using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolanaMusicApi.Application.Requests;

namespace solana_music_api.Controllers;

[Route("api/dashboard")]
[ApiController]
public class DashboardController(IMediator mediator) : ControllerBase
{
    [HttpGet("overview")]
    public async Task<IActionResult> GetOverview()
    {
        var response = await mediator.Send(new GetDashboardOverviewRequest());
        return Ok(response);
    }
    
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        var response = await mediator.Send(new GetUsersRequest());
        return Ok(response);
    }
    
    [HttpGet("artists")]
    public async Task<IActionResult> GetArtists()
    {
        var response = await mediator.Send(new GetArtistsRequest(0));
        return Ok(response);
    }
    
    [HttpGet("tracks")]
    public async Task<IActionResult> GetTracks()
    {
        var response = await mediator.Send(new GetTracksRequest());
        return Ok(response);
    }
    
    [HttpGet("nfts")]
    public async Task<IActionResult> GetNfts(string? type)
    {
        var response = await mediator.Send(new GetNftCollectionsRequest(type));
        return Ok(response);
    }
}