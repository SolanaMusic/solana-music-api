using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.NftServices.NftCollectionService;
using SolanaMusicApi.Domain.DTO.Nft.NftCollection;

namespace SolanaMusicApi.Application.Handlers.Nft;

public class GetArtistNftCollectionsRequestHandler(INftCollectionService nftCollectionService, IMapper mapper) 
    : IRequestHandler<GetArtistNftCollectionsRequest, List<NftCollectionResponseDto>>
{
    public async Task<List<NftCollectionResponseDto>> Handle(GetArtistNftCollectionsRequest request, CancellationToken cancellationToken)
    {
        var response = await nftCollectionService.GetArtistNftCollectionsAsync(request.ArtistId, request.Type, request.Name, request.UserId);
        return mapper.Map<List<NftCollectionResponseDto>>(response);
    }
}