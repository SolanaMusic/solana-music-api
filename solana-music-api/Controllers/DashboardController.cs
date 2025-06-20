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
}