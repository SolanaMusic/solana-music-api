using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.NftServices.NftCollectionService;
using SolanaMusicApi.Domain.DTO.Nft.NftCollection;

namespace SolanaMusicApi.Application.Handlers.Nft;

public class GetNftCollectionsRequestHandler(INftCollectionService nftCollectionService, IMapper mapper) 
    : IRequestHandler<GetNftCollectionsRequest, List<NftCollectionResponseDto>>
{
    public async Task<List<NftCollectionResponseDto>> Handle(GetNftCollectionsRequest request, CancellationToken cancellationToken)
    {
        var response = await nftCollectionService.GetNftCollectionsAsync(request.Type);
        return mapper.Map<List<NftCollectionResponseDto>>(response);
    }
}