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

public class AlbumService : BaseService<Album>, IAlbumService
{
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    private readonly ITracksService _tracksService;
    private readonly IArtistAlbumService _artistAlbumService;

    public AlbumService(IBaseRepository<Album> baseRepository, IMapper mapper, IFileService fileService, ITracksService tracksService,
        IArtistAlbumService artistAlbumService) : base(baseRepository) 
    {
        _mapper = mapper;
        _fileService = fileService;
        _tracksService = tracksService;
        _artistAlbumService = artistAlbumService;
    }

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

        return _mapper.Map<AlbumResponseDto>(response);
    }

    public async Task<AlbumResponseDto> CreateAlbumAsync(AlbumRequestDto albumRequestDto)
    {
        await BeginTransactionAsync();
        var coverPath = await _fileService.SaveFileAsync(albumRequestDto.Cover, FileTypes.AlbumCover);

        try
        {
            var album = _mapper.Map<Album>(albumRequestDto);
            album.ImageUrl = coverPath;

            var added = await AddAsync(album);
            await _artistAlbumService.AddRangeAsync(GetArtistAlbums(albumRequestDto, added));

            if (albumRequestDto.TrackIds != null && albumRequestDto.TrackIds.Any())
                await SetLinkedDataAsync(albumRequestDto, album);

            await CommitTransactionAsync(GetRollBackActions(coverPath));
            var response = await GetAlbumAsync(added.Id);
            return _mapper.Map<AlbumResponseDto>(response);
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
        var coverPath = await _fileService.SaveFileAsync(albumRequestDto.Cover, FileTypes.AlbumCover);

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
            MapProperies(album, albumRequestDto, coverPath);
            var updated = await UpdateAsync(id, album);

            await UpdateLinkedDataAsync(albumRequestDto, album);
            await CommitTransactionAsync(GetRollBackActions(coverPath));
            await ProcessRollBackActions(GetRollBackActions(coverSnapshot));

            var response = await GetAlbumAsync(id);
            return _mapper.Map<AlbumResponseDto>(response);
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
        _fileService.DeleteFile(response.ImageUrl ?? string.Empty);

        return _mapper.Map<AlbumResponseDto>(response);
    }

    public async Task AddToAlbum(long? albumId, long trackId)
    {
        var track = await _tracksService.GetByIdAsync(trackId);
        track.AlbumId = albumId;
        await _tracksService.UpdateAsync(trackId, track);
    }

    private IEnumerable<ArtistAlbum> GetArtistAlbums(AlbumRequestDto albumRequestDto, Album album)
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
            var tracks = await _tracksService.GetAll()
                .Where(x => album.Tracks.Select(t => t.Id).Contains(x.Id))
                .ToListAsync();

            tracks.ForEach(x => x.AlbumId = null);
            await _tracksService.UpdateRangeAsync(tracks);
        }
        else
        {
            var tracks = await _tracksService.GetAll()
                .Where(x => albumRequestDto.TrackIds.Contains(x.Id))
                .ToListAsync();

            tracks.ForEach(x => x.AlbumId = album.Id);
            await _tracksService.UpdateRangeAsync(tracks);
        }
    }

    private async Task UpdateLinkedDataAsync(AlbumRequestDto albumRequestDto, Album album)
    {
        await SetLinkedDataAsync(albumRequestDto, album);

        var existingArtistIds = album.ArtistAlbums.Select(x => x.ArtistId);
        var artistsToAdd = albumRequestDto.ArtistIds.Except(existingArtistIds);
        var artistsToRemove = existingArtistIds.Except(albumRequestDto.ArtistIds);

        if (artistsToRemove.Any())
        {
            var artists = album.ArtistAlbums.Where(x => artistsToRemove.Contains(x.ArtistId)).ToList();
            await _artistAlbumService.DeleteRangeAsync(artists);
        }

        if (artistsToAdd.Any())
        {
            var artists = artistsToAdd.Select(x => new ArtistAlbum { ArtistId = x, AlbumId = album.Id });
            await _artistAlbumService.AddRangeAsync(artists);
        }

    }

    private void MapProperies(Album album, AlbumRequestDto albumRequestDto, string coverPath)
    {
        album.Title = albumRequestDto.Title;
        album.ReleaseDate = albumRequestDto.ReleaseDate;
        album.ImageUrl = coverPath;
        album.Description = albumRequestDto.Description;
    }

    private Func<Task>[] GetRollBackActions(string coverPath) => [() => Task.Run(() => _fileService.DeleteFile(coverPath))];
}
