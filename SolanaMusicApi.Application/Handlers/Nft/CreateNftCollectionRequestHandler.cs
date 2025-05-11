using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.NftServices.NftCollectionService;
using SolanaMusicApi.Application.Services.NftServices.NftService;
using SolanaMusicApi.Domain.DTO.Nft.NftCollection;
using SolanaMusicApi.Domain.Entities.Nft;

namespace SolanaMusicApi.Application.Handlers.Nft;

public class CreateNftCollectionRequestHandler(INftCollectionService nftCollectionService, INftService nftService, IMapper mapper) 
    : IRequestHandler<CreateNftCollectionRequest, NftCollectionResponseDto>
{
    public async Task<NftCollectionResponseDto> Handle(CreateNftCollectionRequest request, CancellationToken cancellationToken)
    {
        var collection = mapper.Map<NftCollection>(request.NftCollectionRequest);
        var nfts = mapper.Map<List<Domain.Entities.Nft.Nft>>(request.NftsRequest);
        collection.Supply = nfts.Count;

        await nftCollectionService.BeginTransactionAsync();

        try
        {
            var createdCollection = await nftCollectionService.AddAsync(collection);
            var generatedNfts = nftCollectionService.GenerateNfts(
                createdCollection, nfts, request.NftCollectionRequest.Price, request.NftCollectionRequest.CurrencyId);

            await nftService.AddRangeAsync(generatedNfts);
            await nftCollectionService.CommitTransactionAsync();
            return mapper.Map<NftCollectionResponseDto>(createdCollection);
        }
        catch (Exception ex)
        {
            await nftCollectionService.RollbackTransactionAsync(ex);
            throw;
        }
    }
}