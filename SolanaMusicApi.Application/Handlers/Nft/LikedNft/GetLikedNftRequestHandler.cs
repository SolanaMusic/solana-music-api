using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.NftServices.LikedNftService;
using SolanaMusicApi.Domain.DTO.Nft.LikedNft;

namespace SolanaMusicApi.Application.Handlers.Nft.LikedNft;

public class GetLikedNftRequestHandler(ILikedNftService likedNftService, IMapper mapper) 
    : IRequestHandler<GetLikedNftRequest, List<LikedNftResponseDto>>
{
    public Task<List<LikedNftResponseDto>> Handle(GetLikedNftRequest request, CancellationToken cancellationToken)
    {
        var response = likedNftService.GetLikedNfts(request.UserId);
        return Task.FromResult(mapper.Map<List<LikedNftResponseDto>>(response));
    }
}