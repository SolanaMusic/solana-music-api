using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolanaMusicApi.Domain.DTO.Payment;
using SolanaMusicApi.Domain.DTO.Transaction;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Domain.DTO.Payment.CryptoWallet;

namespace solana_music_api.Controllers;

[Route("api/payments")]
[ApiController]
public class PaymentsController(IMediator mediator) : ControllerBase
{
    [HttpPost("stripe")]
    public async Task<IActionResult> CreatePaymentSession(SubscriptionPaymentRequestDto subscriptionPaymentRequest)
    {
        var response = await mediator.Send(new CreateStripePaymentSessionRequest(subscriptionPaymentRequest));
        return Ok(response);
    }

    [HttpPost("crypto")]
    public async Task<IActionResult> CreatePaymentSession(CryptoSubscriptionPaymentRequestDto cryptoSubscriptionPaymentRequestDto)
    {
        var response = await mediator.Send(new CryptoPaymentRequest(cryptoSubscriptionPaymentRequestDto));
        return Ok(response);
    }

    [HttpPost("stripe-webhook")]
    public async Task<IActionResult> StripeWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        await mediator.Send(new StripeWebhookRequest(json));
        return NoContent();
    }

    [HttpPost("stripe-refund")]
    public async Task<IActionResult> Refund(RefundTransactionRequestDto refundTransactionRequest)
    {
        await mediator.Send(new StripeRefundRequest(refundTransactionRequest));
        return NoContent();
    }
}
