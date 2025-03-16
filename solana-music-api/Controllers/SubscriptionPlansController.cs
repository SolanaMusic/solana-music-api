using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Domain.DTO.SubscriptionPlan;
using SolanaMusicApi.Domain.DTO.SubscriptionPlanCurrency;

namespace solana_music_api.Controllers;

[Route("api/subscription-plans")]
[ApiController]
public class SubscriptionPlansController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await mediator.Send(new GetSubscriptionPlansRequest());
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await mediator.Send(new GetSubscriptionPlanRequest(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscriptionPlan(CreateSubscriptionPlanRequestDto subscriptionPlanRequestDto)
    {
        var response = await mediator.Send(new CreateSubscriptionPlanRequest(subscriptionPlanRequestDto));
        return Ok(response);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateSubscriptionPlan(long id, UpdateSubscriptionPlanRequestDto subscriptionPlanRequestDto)
    {
        var response = await mediator.Send(new UpdateSubscriptionPlanRequest(id, subscriptionPlanRequestDto));
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubscriptionPlan(long id)
    {
        var response = await mediator.Send(new DeleteSubscriptionPlanRequest(id));
        return Ok(response);
    }

    [HttpPost("add-subscription-currencies{id}")]
    public async Task<IActionResult> AddSubscriptionCurrencies(long id, List<SubscriptionPlanCurrencyRequestDto> subscriptionPlanCurrencyRequestDtos)
    {
        await mediator.Send(new AddSubscriptionCurrenciesRequest(id, subscriptionPlanCurrencyRequestDtos));
        return NoContent();
    }

    [HttpPost("remove-subscription-currencies{id}")]
    public async Task<IActionResult> RemoveSubscriptionCurrencies(long id, List<long> currencyIds)
    {
        await mediator.Send(new RemoveSubscriptionCurrenciesRequest(id, currencyIds));
        return NoContent();
    }
}
