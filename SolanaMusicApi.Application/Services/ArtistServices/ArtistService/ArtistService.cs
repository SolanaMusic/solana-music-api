using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Services.ArtistServices.ArtistSubscriberService;
using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Application.Services.FileService;
using SolanaMusicApi.Domain.DTO.Artist;
using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Domain.Enums.File;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.ArtistServices.ArtistService;

public class ArtistService(IBaseRepository<Artist> baseRepository, IFileService fileService,
    IArtistSubscriberService artistSubscriberService) : BaseService<Artist>(baseRepository), IArtistService
{
    public IQueryable<Artist> GetArtists()
    {
        return GetAll()
            .AsTracking()
            .AsSplitQuery()
            .Include(x => x.User)
                .ThenInclude(x => x != null ? x.Profile : null)
                    .ThenInclude(x => x.Country)
            .Include(x => x.ArtistSubscribers)
                .ThenInclude(x => x.Subscriber)
            .Include(x => x.ArtistAlbums)
                .ThenInclude(x => x.Album)
                    .ThenInclude(x => x.Tracks)
                        .ThenInclude(x => x.TrackGenres)
                            .ThenInclude(x => x.Genre)
            .Include(x => x.ArtistTracks)
                .ThenInclude(x => x.Track)
                    .ThenInclude(x => x.Album)
            .Include(x => x.ArtistTracks)
                .ThenInclude(x => x.Track)
                    .ThenInclude(t => t.ArtistTracks)
                        .ThenInclude(ta => ta.Artist);
    }

    public async Task<Artist> GetArtistAsync(long id)
    {
        return await GetArtists()
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Artist not found");
    }

    public async Task<Artist> CreateArtistAsync(Artist artist, IFormFile? file = null)
    {
        if (file == null)
        {
            artist.Country = null!;
            var response = await AddAsync(artist);
            return await GetArtistAsync(response.Id);
        }
        
        await BeginTransactionAsync();
        var coverPath = await fileService.SaveFileAsync(file, FileTypes.ArtistImage);

        try
        {
            artist.ImageUrl = coverPath;
            artist.Country = null!;
            var response = await AddAsync(artist);

            await CommitTransactionAsync(GetRollBackActions(coverPath));
            return await GetArtistAsync(response.Id);
        }
        catch (Exception ex)
        {
            await RollbackTransactionAsync(ex, GetRollBackActions(coverPath));
            throw;
        }
    }

    public async Task<Artist> UpdateArtistAsync(long id, ArtistRequestDto artistRequestDto)
    {
        await BeginTransactionAsync();
        var coverPath = artistRequestDto.ImageFile != null
            ? await fileService.SaveFileAsync(artistRequestDto.ImageFile, FileTypes.ArtistImage)
            : null;

        try
        {
            var artist = await GetByIdAsync(id);
            var coverSnapshot = artist.ImageUrl;
            MapProperties(artist, artistRequestDto, coverPath);

            await UpdateAsync(artist);
            await CommitTransactionAsync(GetRollBackActions(coverPath));

            if (!string.IsNullOrEmpty(coverSnapshot))
                await ProcessRollBackActions(GetRollBackActions(coverSnapshot));

            return await GetArtistAsync(id);
        }
        catch (Exception ex)
        {
            await RollbackTransactionAsync(ex, GetRollBackActions(coverPath));
            throw;
        }
    }

    public async Task<Artist> DeleteArtistAsync(long id)
    {
        var response = await DeleteAsync(id);

        if (!string.IsNullOrEmpty(response.ImageUrl))
            fileService.DeleteFile(response.ImageUrl);

        return response;
    }

    public bool CheckArtistSubscription(Artist artist, long userId) => 
        artist.ArtistSubscribers.Any(x => x.SubscriberId == userId);

    public async Task SubscribeToArtistAsync(long id, long userId)
    {
        var record = new ArtistSubscriber { ArtistId = id, SubscriberId = userId };
        await artistSubscriberService.AddAsync(record);
    }

    public async Task UnsubscribeFromArtistAsync(long id, long userId) => 
        await artistSubscriberService.DeleteAsync(x => x.ArtistId == id && x.SubscriberId == userId);

    private static void MapProperties(Artist artist, ArtistRequestDto artistRequestDto, string? coverPath)
    {
        artist.Name = artistRequestDto.Name;
        artist.UserId = artistRequestDto.UserId;
        artist.Bio = artistRequestDto.Bio;
        artist.ImageUrl = coverPath;
        artist.CountryId = artistRequestDto.CountryId;
        artist.Country = null!;
    }

    private Func<Task>[] GetRollBackActions(string? coverPath)
    {
        if (!string.IsNullOrEmpty(coverPath))
            return [() => Task.Run(() => fileService.DeleteFile(coverPath))];

        return [];
    }
}
