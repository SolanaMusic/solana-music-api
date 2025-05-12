using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.Nft;

namespace SolanaMusicApi.Application.Services.NftServices.NftCollectionService;

public interface INftCollectionService : IBaseService<NftCollection>
{
    IQueryable<NftCollection> GetNftCollections(string? type);
    Task<List<NftCollection>> GetArtistNftCollectionsAsync(long artistId, string? type, string? name);
    Task<NftCollection> GetNftCollectionAsync(long id);
    List<Nft> GenerateNfts(NftCollection nftCollection, List<Nft> nfts, decimal price, long currencyId);
}