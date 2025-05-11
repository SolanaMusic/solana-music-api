using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.NftServices.NftService;
using SolanaMusicApi.Application.Services.PaymentServices.TransactionService;
using SolanaMusicApi.Domain.DTO.Nft.Nft;

namespace SolanaMusicApi.Application.Handlers.Nft;

public class MintNftRequestHandler(INftService nftService, ITransactionService transactionService, IMapper mapper) 
    : IRequestHandler<MintNftRequest, NftResponseDto>
{
    public async Task<NftResponseDto> Handle(MintNftRequest request, CancellationToken cancellationToken)
    {
        var nft = await nftService.GetNftAsyncById(request.UpdateNftRequest.Id);
        nft.Owner = request.UpdateNftRequest.Owner;
        var response = await nftService.UpdateAsync(nft);

        var transaction = transactionService.MapTransaction(request.TransactionRequest);
        await transactionService.AddAsync(transaction);
        return mapper.Map<NftResponseDto>(response);
    }
}