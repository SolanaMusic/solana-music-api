using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.Nft;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.NftServices.LikedNftService;

public class LikedNftService(IBaseRepository<LikedNft> baseRepository) : BaseService<LikedNft>(baseRepository), ILikedNftService
{
    public IQueryable<LikedNft> GetLikedNfts(long userId)
    {
        return GetAll()
            .Where(x => x.UserId == userId)
            .Include(x => x.Nft)
                .ThenInclude(x => x.Collection)
            .Include(x => x.Nft)
                .ThenInclude(x => x.Currency)
            .Include(x => x.Collection)
                .ThenInclude(x => x.Nfts)
                    .ThenInclude(x => x.Currency);
    }
}