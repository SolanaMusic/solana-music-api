using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolanaMusicApi.Application.Requests.Currency;
using SolanaMusicApi.Domain.DTO.Currency;

namespace solana_music_api.Controllers;

[Route("api/currencies")]
[ApiController]
public class CurrenciesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await mediator.Send(new GetCurrenciesRequest());
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await mediator.Send(new GetCurrencyRequest(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCurrency(CurrencyRequestDto currencyRequestDto)
    {
        var response = await mediator.Send(new CreateCurrencyRequest(currencyRequestDto));
        return Ok(response);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateCurrency(long id, CurrencyRequestDto currencyRequestDto)
    {
        var response = await mediator.Send(new UpdateCurrencyRequest(id, currencyRequestDto));
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCurrency(long id)
    {
        var response = await mediator.Send(new DeleteCurrencyRequest(id));
        return Ok(response);
    }
}
