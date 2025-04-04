using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolanaMusicApi.Application.Requests;

namespace solana_music_api.Controllers;

[Route("api/transactions")]
[ApiController]
public class TransactionsController(IMediator mediator) : ControllerBase
{
    [HttpGet("user-transactions/{userId}")]
    public async Task<IActionResult> GetAll(long userId)
    {
        var response = await mediator.Send(new GetUserTransactionsRequest(userId));
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await mediator.Send(new GetTransactionRequest(id));
        return Ok(response);
    }
}
