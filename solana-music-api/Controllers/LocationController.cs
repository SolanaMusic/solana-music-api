using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolanaMusicApi.Application.Requests.Location;

namespace solana_music_api.Controllers;

[Route("api/location")]
[ApiController]
public class LocationController(IMediator mediator) : ControllerBase
{
    [HttpGet("get-country-name")]
    public async Task<IActionResult> GetUserCountryName()
    {
        var response = await mediator.Send(new CountryNameRequest());
        return Ok(response);
    }

    [HttpGet("get-country-details")]
    public async Task<IActionResult> GetUserCountryDetails()
    {
        var response = await mediator.Send(new CountryDetailsRequest());
        return Ok(response);
    }
}
