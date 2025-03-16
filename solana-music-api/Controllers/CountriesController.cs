using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Domain.DTO.General.CountryDto;

namespace solana_music_api.Controllers;

[Route("api/countries")]
[ApiController]
public class CountriesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await mediator.Send(new GetCountriesRequest());
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await mediator.Send(new GetCountryRequest(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CountryRequestDto countryRequestDto)
    {
        var response = await mediator.Send(new CreateCountryRequest(countryRequestDto));
        return Ok(response);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(long id, CountryRequestDto countryRequestDto)
    {
        var response = await mediator.Send(new UpdateCountryRequest(id, countryRequestDto));
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var response = await mediator.Send(new DeleteCountryRequest(id));
        return Ok(response);
    }
}
