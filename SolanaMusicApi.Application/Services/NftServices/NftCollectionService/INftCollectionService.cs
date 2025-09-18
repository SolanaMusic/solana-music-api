using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.Nft;

namespace SolanaMusicApi.Application.Services.NftServices.NftCollectionService;

public interface INftCollectionService : IBaseService<NftCollection>
{
    IQueryable<NftCollection> GetNftCollections(string? type, long? userId = null);
    Task<List<NftCollection>> GetArtistNftCollectionsAsync(long artistId, string? type, string? name, long? userId = null);
    Task<NftCollection?> GetNftCollectionAsync(long associationId, string type);
    Task<NftCollection> GetNftCollectionAsync(long id, long? userId = null);
    List<Nft> GenerateNfts(NftCollection nftCollection, List<Nft> nfts, decimal price, long currencyId);
}