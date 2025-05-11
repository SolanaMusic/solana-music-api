using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Services.AlbumService;
using SolanaMusicApi.Application.Services.ArtistServices.ArtistService;
using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Application.Services.TrackServices.TracksService;
using SolanaMusicApi.Domain.Entities.Nft;
using SolanaMusicApi.Domain.Enums.Nft;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.NftServices.NftService;

public class NftService(IBaseRepository<Nft> baseRepository, IAlbumService albumService, ITracksService tracksService, 
    IArtistService artistService) : BaseService<Nft>(baseRepository), INftService
{
    public async Task<Nft> GetNftAsyncById(long id)
    {
        var nfts = await GetNftCollectionsQuery(); 
        return nfts.FirstOrDefault(x => x.Id == id) ?? throw new Exception("Nft not found");
    }

    private async Task<List<Nft>> GetNftCollectionsQuery()
    {
        var nfts = await GetAll()
            .Include(x => x.Currency)
            .Include(x => x.Collection)
            .ToListAsync();

        foreach (var nft in nfts)
        {
            switch (nft.Collection.AssociationType)
            {
                case AssociationType.Album:
                    nft.Collection.Album = await albumService.GetAll()
                        .Include(x => x.ArtistAlbums)
                        .ThenInclude(x => x.Artist)
                        .Where(x => x.Id == nft.Collection.AssociationId)
                        .FirstOrDefaultAsync();
                    break;
                case AssociationType.Track:
                    nft.Collection.Track = await tracksService.GetAll()
                        .Include(x => x.ArtistTracks)
                        .ThenInclude(x => x.Artist)
                        .Where(x => x.Id == nft.Collection.AssociationId)
                        .FirstOrDefaultAsync();
                    break;
                case AssociationType.Artist:
                    nft.Collection.Artist = await artistService.GetByIdAsync(nft.Collection.AssociationId);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            nft.Collection.Nfts = null!;
        }
        
        return nfts;
    }
}