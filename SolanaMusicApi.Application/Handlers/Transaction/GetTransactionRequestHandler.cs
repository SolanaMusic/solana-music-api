using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.PaymentServices.TransactionService;
using SolanaMusicApi.Domain.DTO.Transaction;

namespace SolanaMusicApi.Application.Handlers.Transaction;

public class GetTransactionRequestHandler(ITransactionService transactionService, IMapper mapper) : 
    IRequestHandler<GetTransactionRequest, TransactionResponseDto>
{
    public async Task<TransactionResponseDto> Handle(GetTransactionRequest request, CancellationToken cancellationToken)
    {
        var response = await transactionService.GetTransactionAsync(request.Id);
        return mapper.Map<TransactionResponseDto>(response);
    }
}
