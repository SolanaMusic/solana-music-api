using Microsoft.AspNetCore.Http;
using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.DTO.Artist;
using SolanaMusicApi.Domain.Entities.Performer;

namespace SolanaMusicApi.Application.Services.ArtistServices.ArtistService;

public interface IArtistService : IBaseService<Artist>
{
    IQueryable<Artist> GetArtists();
    Task<Artist> GetArtistAsync(long id);
    bool CheckArtistSubscription(Artist artist, long userId);
    Task<Artist> CreateArtistAsync(Artist artist, IFormFile? file);
    Task<Artist> UpdateArtistAsync(long id, ArtistRequestDto artistRequestDto);
    Task<Artist> DeleteArtistAsync(long id);
    Task SubscribeToArtistAsync(long id, long userId);
    Task UnsubscribeFromArtistAsync(long id, long userId);
}
