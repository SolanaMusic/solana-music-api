using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Domain.DTO.Dashboard;
using SolanaMusicApi.Domain.DTO.Sorting;
using SolanaMusicApi.Domain.Enums;

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
    public async Task<IActionResult> GetUsers(
        [FromQuery] DashboardFilter filter, 
        [FromQuery] RequestSortingDto sorting)
    {
        var response = await mediator.Send(new GetUsersRequest(filter, sorting));
        return Ok(response);
    }
    
    [HttpGet("artists")]
    public async Task<IActionResult> GetArtists(
        [FromQuery] DashboardFilter filter,
        [FromQuery] RequestSortingDto sorting)
    {
        var response = await mediator.Send(new GetArtistsDashboardRequest(filter, sorting));
        return Ok(response);
    }
    
    [HttpGet("tracks")]
    public async Task<IActionResult> GetTracks(
        [FromQuery] DashboardFilter filter,
        [FromQuery] RequestSortingDto sorting)
    {
        var response = await mediator.Send(new GetDashboardTracksRequest(filter, sorting));
        return Ok(response);
    }
    
    [HttpGet("nfts")]
    public async Task<IActionResult> GetNfts(
        [FromQuery] DashboardFilter filter,
        [FromQuery] RequestSortingDto sorting, 
        string? type)
    {
        var response = await mediator.Send(new GetDashboardNftsRequest(filter, sorting, type));
        return Ok(response);
    }
    
    [HttpGet("applications")]
    public async Task<IActionResult> GetArtistApplications(
        [FromQuery] DashboardFilter filter,
        [FromQuery] RequestSortingDto sorting,
        ArtistApplicationStatus? status)
    {
        var response = await mediator.Send(new GetArtistApplicationsRequest(filter, sorting, status));
        return Ok(response);
    }
    
    [HttpGet("active-applications")]
    public async Task<IActionResult> GetPendingArtistApplications()
    {
        var response = await mediator.Send(new GetPendingApplications());
        return Ok(response);
    }
}