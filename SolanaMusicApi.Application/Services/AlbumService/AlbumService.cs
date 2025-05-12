using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Services.ArtistServices.ArtistAlbumService;
using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Application.Services.FileService;
using SolanaMusicApi.Application.Services.TrackServices.TracksService;
using SolanaMusicApi.Domain.DTO.Album;
using SolanaMusicApi.Domain.Entities.Music;
using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Domain.Enums.File;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.AlbumService;

public class AlbumService(IBaseRepository<Album> baseRepository, IMapper mapper, IFileService fileService,
    ITracksService tracksService, IArtistAlbumService artistAlbumService) : BaseService<Album>(baseRepository), IAlbumService
{
    public IQueryable<Album> GetAlbums()
    {
        return GetAll()
            .Include(x => x.ArtistAlbums)
                .ThenInclude(x => x.Artist)

            .Include(x => x.Tracks)
                .ThenInclude(x => x.ArtistTracks)
                    .ThenInclude(x => x.Artist)

            .Include(x => x.Tracks)
                .ThenInclude(x => x.TrackGenres)
                    .ThenInclude(x => x.Genre);
    }

    public async Task<AlbumResponseDto> GetAlbumAsync(long id)
    {
        var response = await GetAlbums()
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Album not found");

        return mapper.Map<AlbumResponseDto>(response);
    }

    public async Task<AlbumResponseDto> CreateAlbumAsync(AlbumRequestDto albumRequestDto)
    {
        await BeginTransactionAsync();
        var coverPath = await fileService.SaveFileAsync(albumRequestDto.Cover, FileTypes.AlbumCover);

        try
        {
            var album = mapper.Map<Album>(albumRequestDto);
            album.ImageUrl = coverPath;

            var added = await AddAsync(album);
            await artistAlbumService.AddRangeAsync(GetArtistAlbums(albumRequestDto, added));

            if (albumRequestDto.TrackIds != null && albumRequestDto.TrackIds.Any())
                await SetLinkedDataAsync(albumRequestDto, album);

            await CommitTransactionAsync(GetRollBackActions(coverPath));
            var response = await GetAlbumAsync(added.Id);
            return mapper.Map<AlbumResponseDto>(response);
        }
        catch (Exception ex)
        {
            await RollbackTransactionAsync(ex, GetRollBackActions(coverPath));
            throw;
        }
    }

    public async Task<AlbumResponseDto> UpdateAlbumAsync(long id, AlbumRequestDto albumRequestDto)
    {
        await BeginTransactionAsync();
        var coverPath = await fileService.SaveFileAsync(albumRequestDto.Cover, FileTypes.AlbumCover);

        try
        {
            var album = await GetAll()
                .Include(x => x.Tracks)
                .Include(x => x.ArtistAlbums)
                    .ThenInclude(x => x.Artist)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (album == null)
                throw new Exception("Album not found");

            var coverSnapshot = album.ImageUrl;
            MapProperties(album, albumRequestDto, coverPath);
            await UpdateAsync(id, album);

            await UpdateLinkedDataAsync(albumRequestDto, album);
            await CommitTransactionAsync(GetRollBackActions(coverPath));
            await ProcessRollBackActions(GetRollBackActions(coverSnapshot));

            var response = await GetAlbumAsync(id);
            return mapper.Map<AlbumResponseDto>(response);
        }
        catch (Exception ex)
        {
            await RollbackTransactionAsync(ex, GetRollBackActions(coverPath));
            throw;
        }
    }

    public async Task<AlbumResponseDto> DeleteAlbumAsync(long id)
    {
        var response = await DeleteAsync(id);
        fileService.DeleteFile(response.ImageUrl);
        return mapper.Map<AlbumResponseDto>(response);
    }

    public async Task AddToAlbumAsync(AddToAlbumDto addToAlbumDto)
    {
        var track = await tracksService.GetByIdAsync(addToAlbumDto.TrackId);
        track.AlbumId = addToAlbumDto.AlbumId;
        await tracksService.UpdateAsync(track);
    }

    public async Task RemoveFromAlbumAsync(long trackId)
    {
        var track = await tracksService.GetByIdAsync(trackId);
        track.AlbumId = null;
        await tracksService.UpdateAsync(track);
    }

    private static IEnumerable<ArtistAlbum> GetArtistAlbums(AlbumRequestDto albumRequestDto, Album album)
    {
        return albumRequestDto.ArtistIds.Select(artistId => new ArtistAlbum
        {
            ArtistId = artistId,
            AlbumId = album.Id
        });
    }

    private async Task SetLinkedDataAsync(AlbumRequestDto albumRequestDto, Album album)
    {
        if (albumRequestDto.TrackIds == null || !albumRequestDto.TrackIds.Any())
        {
            var tracks = await tracksService.GetAll()
                .Where(x => album.Tracks.Select(t => t.Id).Contains(x.Id))
                .ToListAsync();

            tracks.ForEach(x => x.AlbumId = null);
            await tracksService.UpdateRangeAsync(tracks);
        }
        else
        {
            var tracks = await tracksService.GetAll()
                .Where(x => albumRequestDto.TrackIds.Contains(x.Id))
                .ToListAsync();

            tracks.ForEach(x => x.AlbumId = album.Id);
            await tracksService.UpdateRangeAsync(tracks);
        }
    }

    private async Task UpdateLinkedDataAsync(AlbumRequestDto albumRequestDto, Album album)
    {
        await SetLinkedDataAsync(albumRequestDto, album);

        var existingArtistIds = album.ArtistAlbums.Select(x => x.ArtistId).ToList();
        var artistsToAdd = albumRequestDto.ArtistIds.Except(existingArtistIds).ToList();
        var artistsToRemove = existingArtistIds.Except(albumRequestDto.ArtistIds).ToList();

        if (artistsToRemove.Count != 0)
        {
            var artists = album.ArtistAlbums.Where(x => artistsToRemove.Contains(x.ArtistId)).ToList();
            await artistAlbumService.DeleteRangeAsync(artists);
        }

        if (artistsToAdd.Count != 0)
        {
            var artists = artistsToAdd.Select(x => new ArtistAlbum { ArtistId = x, AlbumId = album.Id });
            await artistAlbumService.AddRangeAsync(artists);
        }

    }

    private static void MapProperties(Album album, AlbumRequestDto albumRequestDto, string coverPath)
    {
        album.Title = albumRequestDto.Title;
        album.ReleaseDate = albumRequestDto.ReleaseDate;
        album.ImageUrl = coverPath;
        album.Description = albumRequestDto.Description;
    }

    private Func<Task>[] GetRollBackActions(string coverPath) => [() => Task.Run(() => fileService.DeleteFile(coverPath))];
}
