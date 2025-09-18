using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.NftServices.LikedNftService;

namespace SolanaMusicApi.Application.Handlers.Nft.LikedNft;

public class AddLikedNftRequestHandler(ILikedNftService likedNftService, IMapper mapper) : IRequestHandler<AddLikedNftRequest>
{
    public async Task Handle(AddLikedNftRequest request, CancellationToken cancellationToken)
    {
        var likedNft = mapper.Map<Domain.Entities.Nft.LikedNft>(request.LikedNftRequestDto);
        await likedNftService.AddAsync(likedNft);
    }
}