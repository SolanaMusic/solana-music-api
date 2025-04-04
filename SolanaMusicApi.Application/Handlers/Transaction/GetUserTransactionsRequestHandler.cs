using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.PaymentServices.TransactionService;
using SolanaMusicApi.Domain.DTO.Transaction;

namespace SolanaMusicApi.Application.Handlers.Transaction;

public class GetUserTransactionsRequestHandler(ITransactionService transactionService, IMapper mapper) : 
    IRequestHandler<GetUserTransactionsRequest, List<TransactionResponseDto>>
{
    public async Task<List<TransactionResponseDto>> Handle(GetUserTransactionsRequest request, CancellationToken cancellationToken)
    {
        var response = await transactionService.GetUserTransactions(request.UserId).ToListAsync();
        return mapper.Map<List<TransactionResponseDto>>(response);
    }
}
