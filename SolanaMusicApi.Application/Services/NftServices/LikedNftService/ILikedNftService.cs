using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.Nft;

namespace SolanaMusicApi.Application.Services.NftServices.LikedNftService;

public interface ILikedNftService : IBaseService<LikedNft>
{
    IQueryable<LikedNft> GetLikedNfts(long userId);
}