using MediatR;
using SolanaMusicApi.Domain.DTO.Transaction;

namespace SolanaMusicApi.Application.Requests;

public record GetUserTransactionsRequest(long UserId) : IRequest<List<TransactionResponseDto>>;

public record GetTransactionRequest(long Id) : IRequest<TransactionResponseDto>;
