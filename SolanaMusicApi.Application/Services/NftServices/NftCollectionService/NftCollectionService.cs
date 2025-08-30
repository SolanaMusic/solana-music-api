using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Services.AlbumService;
using SolanaMusicApi.Application.Services.ArtistServices.ArtistService;
using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Application.Services.TrackServices.TracksService;
using SolanaMusicApi.Domain.Constants;
using SolanaMusicApi.Domain.Entities.Nft;
using SolanaMusicApi.Domain.Enums.Nft;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.NftServices.NftCollectionService;

public class NftCollectionService(IBaseRepository<NftCollection> baseRepository, IAlbumService albumService,
    ITracksService tracksService, IArtistService artistService) : BaseService<NftCollection>(baseRepository), INftCollectionService
{
    public IQueryable<NftCollection> GetNftCollections(string? type)
    {
        var query = GetNftCollectionsQuery();

        if (string.IsNullOrEmpty(type)) 
            return query;
        
        var associationType = GetAssociationType(type);
        query = query.Where(x => x.AssociationType == associationType);

        return query;
    }
    
    public async Task<List<NftCollection>> GetArtistNftCollectionsAsync(long artistId, string? type, string? name)
    {
        var collections = GetNftCollections(type);

        if (!string.IsNullOrEmpty(name))
            collections = collections.Where(x => EF.Functions.Like(x.Name, $"%{name}%"));
        
        if (string.IsNullOrEmpty(type))
        {
            var filtered = await collections.ToListAsync();

            return filtered.Where(x =>
                (x.Artist != null && x.Artist.Id == artistId)
                || (x.Album != null && x.Album.ArtistAlbums.Any(aa => aa.ArtistId == artistId))
                || (x.Track != null && x.Track.ArtistTracks.Any(at => at.ArtistId == artistId))
            ).ToList();
        }

        var associationType = GetAssociationType(type);
        var filteredByType = await collections.ToListAsync();

        return associationType switch
        {
            AssociationType.Artist => filteredByType.Where(x => x.Artist != null && x.Artist.Id == artistId).ToList(),

            AssociationType.Album => filteredByType.Where(x => x.Album != null && x.Album.ArtistAlbums
                .Any(aa => aa.ArtistId == artistId)).ToList(),

            AssociationType.Track => filteredByType.Where(x => x.Track != null && x.Track.ArtistTracks
                .Any(at => at.ArtistId == artistId)).ToList(),

            _ => throw new ArgumentOutOfRangeException(nameof(associationType), associationType, "Invalid association type")
        };
    }

    public async Task<NftCollection> GetNftCollectionAsync(long id)
    {
        return await GetNftCollectionsQuery().FirstOrDefaultAsync(x => x.Id == id)
               ?? throw new Exception("Nft collection not found");
    }
    
    public List<Nft> GenerateNfts(NftCollection collection, List<Nft> nfts, decimal price, long currencyId)
    {
        var random = new Random();

        return nfts
            .Select(nft =>
            {
                nft.CollectionId = collection.Id;
                nft.CurrencyId = currencyId;
                
                var randomValue = random.NextDouble();
                var rarity = Rarity.Common;
                var cumulativeProbability = 0.0;

                foreach (var rarityProb in Constants.RarityProbabilities)
                {
                    cumulativeProbability += rarityProb.Value;
                    if (!(randomValue <= cumulativeProbability))
                        continue;

                    rarity = rarityProb.Key;
                    break;
                }

                nft.Rarity = rarity;
                var priceIncreaseFactor = rarity switch
                {
                    Rarity.Common => 1.0m,
                    Rarity.Rare => 1.2m,
                    Rarity.Epic => 1.5m,
                    Rarity.Mythic => 2.0m,
                    Rarity.Legendary => 3.0m,
                    _ => 1.0m,
                };

                var priceIncrease = (decimal)(random.NextDouble() * (0.5 * (int)rarity));
                nft.Price = price * priceIncreaseFactor * (1 + priceIncrease);

                return nft;
            })
            .ToList();
    }

    private IQueryable<NftCollection> GetNftCollectionsQuery()
    {
        var albums = albumService.GetAll()
            .Include(x => x.ArtistAlbums)
            .ThenInclude(x => x.Artist);

        var tracks = tracksService.GetAll()
            .Include(x => x.ArtistTracks)
            .ThenInclude(x => x.Artist);

        var artists = artistService.GetAll();

        return from c in GetAll()
                .Include(x => x.Nfts)
                .ThenInclude(n => n.Currency)
                .AsSplitQuery()
            join a in albums on c.AssociationId equals a.Id into albumJoin
            from album in albumJoin.DefaultIfEmpty()
            join t in tracks on c.AssociationId equals t.Id into trackJoin
            from track in trackJoin.DefaultIfEmpty()
            join ar in artists on c.AssociationId equals ar.Id into artistJoin
            from artist in artistJoin.DefaultIfEmpty()
            orderby c.Nfts.Count < c.Supply descending
            select new NftCollection
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                Supply = c.Supply,
                AssociationId = c.AssociationId,
                AssociationType = c.AssociationType,
                ImageUrl = c.ImageUrl,
                Nfts = c.Nfts,
                Album = c.AssociationType == AssociationType.Album ? album : null,
                Track = c.AssociationType == AssociationType.Track ? track : null,
                Artist = c.AssociationType == AssociationType.Artist ? artist : null,
            };
    }

    private static AssociationType GetAssociationType(string associationType) => 
        (AssociationType)Enum.Parse(typeof(AssociationType), associationType, true);
}