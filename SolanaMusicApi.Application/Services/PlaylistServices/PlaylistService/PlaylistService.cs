using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Application.Services.FileService;
using SolanaMusicApi.Application.Services.PlaylistServices.PlaylistTrackService;
using SolanaMusicApi.Domain.DTO.Playlist;
using SolanaMusicApi.Domain.Entities.Playlist;
using SolanaMusicApi.Domain.Enums.File;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;
using System.Linq.Expressions;

namespace SolanaMusicApi.Application.Services.PlaylistServices.PlaylistService;

public class PlaylistService : BaseService<Playlist>, IPlaylistService
{
    private readonly IFileService _fileService;
    private readonly IPlaylistTrackService _playlistTrackService;

    public PlaylistService(IBaseRepository<Playlist> baseRepository, IFileService fileService, IPlaylistTrackService playlistTrackService) : base(baseRepository) 
    {
        _fileService = fileService;
        _playlistTrackService = playlistTrackService;
    }

    public IQueryable<Playlist> GetPlaylists(long userId)
    {
        return GetPlaylistsQuery()
            .Where(x => x.OwnerId == userId);
    }

    public async Task<Playlist> GetPlaylistAsync(long id)
    {
        return await GetPlaylistsQuery()
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Playlist not found");
    }

    public async Task<Playlist> CreatePlaylistAsync(Playlist playlist, CreatePlaylistRequestDto playlistRequestDto)
    {
        await BeginTransactionAsync();
        var coverPath = playlistRequestDto.CoverFile != null
            ? await _fileService.SaveFileAsync(playlistRequestDto.CoverFile, FileTypes.PlaylistCover)
            : string.Empty;

        try
        {
            playlist.CoverUrl = coverPath;
            var added = await AddAsync(playlist);
            await CommitTransactionAsync(GetRollBackActions(coverPath));

            var response = await GetPlaylistAsync(added.Id);
            return response;
        }
        catch (Exception ex)
        {
            await RollbackTransactionAsync(ex, GetRollBackActions(coverPath));
            throw;
        }
    }

    public async Task<Playlist> UpdatePlaylistAsync(long id, UpdatePlaylistRequestDto playlistRequestDto)
    {
        await BeginTransactionAsync();
        var coverPath = playlistRequestDto.CoverFile != null
            ? await _fileService.SaveFileAsync(playlistRequestDto.CoverFile, FileTypes.PlaylistCover)
            : string.Empty;

        try
        {
            var playlist = await GetByIdAsync(id);
            var coverSnapshot = playlist.CoverUrl;
            playlist.CoverUrl = coverPath;
            playlist.Name = playlistRequestDto.Name;

            var updated = await UpdateAsync(id, playlist);
            await CommitTransactionAsync(GetRollBackActions(coverPath));
            
            if (!string.IsNullOrEmpty(coverSnapshot))
                await ProcessRollBackActions(GetRollBackActions(coverSnapshot));

            var response = await GetPlaylistAsync(updated.Id);
            return response;
        }
        catch (Exception ex)
        {
            await RollbackTransactionAsync(ex, GetRollBackActions(coverPath));
            throw;
        }
    }

    public async Task<Playlist> DeletePlaylistAsync(long id)
    {
        var response = await DeleteAsync(id);
        
        if (!string.IsNullOrEmpty(response.CoverUrl))
            _fileService.DeleteFile(response.CoverUrl);

        return response;
    }

    public async Task AddToPlaylistAsync(AddToPlaylistDto addToPlaylistDto)
    {
        var record = new PlaylistTrack { PlaylistId = addToPlaylistDto.PlaylistId, TrackId = addToPlaylistDto.TrackId };
        await _playlistTrackService.AddAsync(record);
    }

    public async Task RemoveFromPlaylistAsync(AddToPlaylistDto addToPlaylistDto)
    {
        Expression<Func<PlaylistTrack, bool>> expression = x => x.PlaylistId == addToPlaylistDto.PlaylistId 
            && x.TrackId == addToPlaylistDto.TrackId;

        await _playlistTrackService.DeleteAsync(expression);
    }

    private IQueryable<Playlist> GetPlaylistsQuery()
    {
        return GetAll()
            .Include(x => x.PlaylistTracks)
                .ThenInclude(x => x.Track)
                    .ThenInclude(x => x.ArtistTracks)
                        .ThenInclude(x => x.Artist)

            .Include(x => x.Owner)
                .ThenInclude(x => x.Profile)

            .Select(x => new Playlist
            {
                Id = x.Id,
                Name = x.Name,
                PlaylistTracks = x.PlaylistTracks,
                Owner = x.Owner,
                CoverUrl = !string.IsNullOrEmpty(x.CoverUrl)
                    ? x.CoverUrl
                    : x.PlaylistTracks.Select(pt => pt.Track.ImageUrl).FirstOrDefault()
            });
    }

    private Func<Task>[] GetRollBackActions(string coverPath) => [() => Task.Run(() => _fileService.DeleteFile(coverPath))];
}
