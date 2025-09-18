using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.NftServices.NftCollectionService;
using SolanaMusicApi.Domain.DTO.Nft.NftCollection;

namespace SolanaMusicApi.Application.Handlers.Nft;

public class GetNftCollectionRequestHandler(INftCollectionService nftCollectionService, IMapper mapper) 
    : IRequestHandler<GetNftCollectionRequest,NftCollectionResponseDto>
{
    public async Task<NftCollectionResponseDto> Handle(GetNftCollectionRequest request, CancellationToken cancellationToken)
    {
        var response = await nftCollectionService.GetNftCollectionAsync(request.Id, request.UserId);
        return mapper.Map<NftCollectionResponseDto>(response);
    }
}