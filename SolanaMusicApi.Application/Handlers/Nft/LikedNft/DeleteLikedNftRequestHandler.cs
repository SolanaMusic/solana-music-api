using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.NftServices.LikedNftService;

namespace SolanaMusicApi.Application.Handlers.Nft.LikedNft;

public class DeleteLikedNftRequestHandler(ILikedNftService likedNftService) : IRequestHandler<DeleteLikedNftRequest>
{
    public async Task Handle(DeleteLikedNftRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Type))
        {
            await likedNftService.DeleteAsync(request.Id);
            return;
        }

        switch (request.Type.ToLower())
        {
            case "nft":
            {
                await likedNftService.DeleteAsync(x => x.NftId == request.Id);
                return;
            }
            case "collection":
            {
                await likedNftService.DeleteAsync(x => x.CollectionId == request.Id);
                return;
            }
        }
    }
}