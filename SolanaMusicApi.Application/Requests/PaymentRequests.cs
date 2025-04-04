using MediatR;
using SolanaMusicApi.Domain.DTO.Payment;
using SolanaMusicApi.Domain.DTO.Transaction;

namespace SolanaMusicApi.Application.Requests;

public record CreateStripePaymentSessionRequest(SubscriptionPaymentRequestDto SubscriptionPaymentRequest) : IRequest<string>;
public record StripeWebhookRequest(string Json) : IRequest;
public record StripeRefundRequest(RefundTransactionRequestDto RefundTransactionRequest) : IRequest;