using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.AlbumService;
using SolanaMusicApi.Application.Services.NftServices.NftCollectionService;
using SolanaMusicApi.Domain.DTO.Album;
using SolanaMusicApi.Domain.DTO.Nft.NftCollection;

namespace SolanaMusicApi.Application.Handlers.Album;

public class GetAlbumRequestHandler(IAlbumService albumService, INftCollectionService nftCollectionService, IMapper mapper) 
    : IRequestHandler<GetAlbumRequest, AlbumResponseDto>
{
    public async Task<AlbumResponseDto> Handle(GetAlbumRequest request, CancellationToken cancellationToken)
    {
        var response = await albumService.GetAlbumAsync(request.Id);
        var nft = await nftCollectionService.GetNftCollectionAsync(request.Id, "album");
        response.NftCollection = mapper.Map<NftCollectionResponseDto>(nft);
        
        return response;
    }
}
