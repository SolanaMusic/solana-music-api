using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Domain.DTO.Subscription;

namespace solana_music_api.Controllers;

[Route("api/subscriptions")]
[ApiController]
public class SubscriptionsController(IMediator mediator) : ControllerBase
{
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAll(long userId)
    {
        var response = await mediator.Send(new GetSubscriptionsRequest(userId));
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery]SubscriptionActionDto subscriptionActionDto)
    {
        var response = await mediator.Send(new GetSubscriptionRequest(subscriptionActionDto));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscription(SubscriptionRequestDto subscriptionRequestDto)
    {
        var response = await mediator.Send(new CreateSubscriptionRequest(subscriptionRequestDto));
        return Ok(response);
    }

    [HttpPost("add-to-subscription")]
    public async Task<IActionResult> AddToSubscription(SubscriptionActionsDto subscriptionAction)
    {
        await mediator.Send(new AddToSubscriptionRequest(subscriptionAction));
        return NoContent();
    }

    [HttpPost("remove-from-subscription")]
    public async Task<IActionResult> RemoveFromSubscription(SubscriptionActionsDto subscriptionAction)
    {
        await mediator.Send(new RemoveFromSubscriptionRequest(subscriptionAction));
        return NoContent();
    }

    [HttpPost("select-subscription")]
    public async Task<IActionResult> SelectSubscription(SubscriptionActionDto subscriptionActionDto)
    {
        await mediator.Send(new SelectSubscriptionRequest(subscriptionActionDto));
        return NoContent();
    }

    [HttpPost("resubscribe")]
    public async Task<IActionResult> Resubscribe([FromBody]ResubscribeRequestDto resubscribeRequestDto)
    {
        await mediator.Send(new ResubscribeRequest(resubscribeRequestDto.Id));
        return NoContent();
    }
}
