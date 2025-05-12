using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Services.ArtistServices.ArtistService;
using SolanaMusicApi.Application.Services.ArtistServices.ArtistTrackService;
using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Application.Services.FileService;
using SolanaMusicApi.Application.Services.GenreService;
using SolanaMusicApi.Application.Services.TrackServices.TrackGenreService;
using SolanaMusicApi.Domain.DTO.Track;
using SolanaMusicApi.Domain.Entities.Music;
using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Domain.Enums.File;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.TrackServices.TracksService;

public class TracksService(IBaseRepository<Track> baseRepository, ITrackGenreService trackGenreService, IFileService fileService,
    IGenreService genreService, IArtistService artistService, IArtistTrackService artistTrackService, IMapper mapper)
    : BaseService<Track>(baseRepository), ITracksService
{
    public async Task<TrackResponseDto> GetTrackAsync(long id)
    {
        var trackQuery = await GetTracksQueryAsync();
        var track = await trackQuery.FirstOrDefaultAsync(t => t.Id == id) ?? throw new KeyNotFoundException("Track not found");
        MapProperties(track);

        return mapper.Map<TrackResponseDto>(track);
    }

    public async Task<FileStream> GetTrackFileStreamAsync(long id)
    {
        var response = await GetByIdAsync(id);
        var filePath = response.FileUrl;

        if (!Path.IsPathRooted(filePath))
            filePath = Path.Combine("wwwroot", filePath);

        return new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
    }

    public async Task<TrackResponseDto> CreateTrackAsync(TrackRequestDto trackRequestDto)
    {
        await BeginTransactionAsync();
        var (coverPath, trackPath) = await SaveTrackFilesAsync(trackRequestDto);

        try
        {
            var track = MapTrack(trackRequestDto, trackPath, coverPath);
            var added = await AddAsync(track);
            await InsertLinkedDataAsync(added, trackRequestDto);
            await CommitTransactionAsync(GetRollBackActions(coverPath, trackPath));

            var response = await GetTrackAsync(added.Id);
            return mapper.Map<TrackResponseDto>(response);
        }
        catch (Exception ex)
        {
            await RollbackTransactionAsync(ex, GetRollBackActions(coverPath, trackPath));
            throw;
        }
    }

    public async Task<TrackResponseDto> UpdateTrackAsync(long id, TrackRequestDto trackRequestDto)
    {
        await BeginTransactionAsync();
        var (coverPath, trackPath) = await SaveTrackFilesAsync(trackRequestDto);

        try
        {
            var track = await GetAll()
                .Include(x => x.TrackGenres)
                .Include(x => x.ArtistTracks)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (track == null)
                throw new Exception("Track not found");

            var (imageSnapshot, fileSnapshot) = (track.ImageUrl, track.FileUrl);
            await MapTrackAsync(track, trackRequestDto, trackPath, coverPath);

            await UpdateAsync(track);
            await CommitTransactionAsync(GetRollBackActions(coverPath, trackPath));
            await ProcessRollBackActions(GetRollBackActions(imageSnapshot, fileSnapshot));

            var response = await GetTrackAsync(id);
            return mapper.Map<TrackResponseDto>(response);
        }
        catch (Exception ex)
        {
            await RollbackTransactionAsync(ex, GetRollBackActions(coverPath, trackPath));
            throw;
        }
    }

    public async Task<TrackResponseDto> DeleteTrackAsync(long id)
    {
        var response = await DeleteAsync(id);
        await Task.WhenAll(GetRollBackActions(response.ImageUrl ?? string.Empty, response.FileUrl).Select(action => action()));

        return mapper.Map<TrackResponseDto>(response);
    }

    public async Task<IQueryable<Track>> GetTracksQueryAsync()
    {
        var trackQuery = GetAll()
            .Include(t => t.ArtistTracks)
                .ThenInclude(x => x.Artist)
                    .ThenInclude(ar => ar.Country)

            .Include(t => t.TrackGenres)
                .ThenInclude(x => x.Genre)

            .Include(t => t.Album)
            .AsQueryable();

        if (await trackQuery.AnyAsync(t => t.Album != null))
        {
            trackQuery = trackQuery
                .Include(t => t.Album!.ArtistAlbums)
                    .ThenInclude(x => x.Artist)
                        .ThenInclude(ar => ar.Country);
        }

        return trackQuery;
    }
    
    public void MapProperties(Track track)
    {
        if (string.IsNullOrEmpty(track.ImageUrl) && track.Album?.ImageUrl != null)
            track.ImageUrl = track.Album.ImageUrl;

        var artists = track.Album?.ArtistAlbums
            .Select(x => x.Artist)
            .ToList();
        
        if (track.ArtistTracks.Count != 0 || artists == null || artists.Count == 0) 
            return;

        if (track.ArtistTracks is List<ArtistTrack> artistTracksList)
            artistTracksList.AddRange(artists.Select(artist => new ArtistTrack { Track = track, Artist = artist }));
    }

    private Track MapTrack(TrackRequestDto trackRequestDto, string trackFile, string? coverFile)
    {
        var track = mapper.Map<Track>(trackRequestDto);
        track.FileUrl = trackFile;
        track.ImageUrl = coverFile;
        track.Duration = fileService.GetAudioDuration(trackRequestDto.TrackFile);

        return track;
    }

    private async Task MapTrackAsync(Track track, TrackRequestDto trackRequestDto, string trackFile, string? coverFile)
    {
        track.Title = trackRequestDto.Title;
        track.AlbumId = trackRequestDto.AlbumId;
        track.FileUrl = trackFile;
        track.ImageUrl = coverFile;
        track.Duration = fileService.GetAudioDuration(trackRequestDto.TrackFile);

        await UpdateLinkedDataAsync(track, trackRequestDto);
    }

    private async Task<Track> UpdateLinkedDataAsync(Track track, TrackRequestDto trackRequestDto)
    {
        var existingGenres = track.TrackGenres
            .Select(x => x.GenreId)
            .ToList();
        
        var genresToAdd = trackRequestDto.GenreIds
            .Except(existingGenres)
            .ToList();
        
        var genresToRemove = existingGenres
            .Except(trackRequestDto.GenreIds)
            .ToList();

        var existingArtists = track.ArtistTracks
            .Select(x => x.ArtistId)
            .ToList();
        
        var artistsToAdd = trackRequestDto.ArtistIds.Except(existingArtists).ToList();
        var artistsToRemove = existingArtists.Except(trackRequestDto.ArtistIds).ToList();

        if (genresToRemove.Count != 0)
        {
            var genresToDelete = track.TrackGenres.Where(x => genresToRemove.Contains(x.GenreId)).ToList();
            await trackGenreService.DeleteRangeAsync(genresToDelete);
        }

        if (artistsToRemove.Count != 0)
        {
            var artistsToDelete = track.ArtistTracks.Where(x => artistsToRemove.Contains(x.ArtistId)).ToList();
            await artistTrackService.DeleteRangeAsync(artistsToDelete);
        }

        if (genresToAdd.Count == 0 && artistsToAdd.Count == 0) 
            return track;
        
        trackRequestDto.GenreIds = genresToAdd;
        trackRequestDto.ArtistIds = artistsToAdd;
        await InsertLinkedDataAsync(track, trackRequestDto);

        return track;
    }

    private async Task InsertLinkedDataAsync(Track track, TrackRequestDto trackRequestDto)
    {
        var (genres, artists) = GetLinkedData(track, trackRequestDto);
        await trackGenreService.AddRangeAsync(genres);
        await artistTrackService.AddRangeAsync(artists);
    }

    private (IQueryable<TrackGenre>, IQueryable<ArtistTrack>) GetLinkedData(Track track, TrackRequestDto trackRequestDto)
    {
        var genres = genreService
            .GetAll()
            .Where(x => trackRequestDto.GenreIds.Contains(x.Id));

        var artists = artistService
            .GetAll()
            .Where(x => trackRequestDto.ArtistIds.Contains(x.Id));

        var trackGenres = genres.Select(genre => new TrackGenre { TrackId = track.Id, GenreId = genre.Id });
        var artistTracks = artists.Select(artist => new ArtistTrack { ArtistId = artist.Id, TrackId = track.Id });

        return (trackGenres, artistTracks);
    }

    private async Task<(string?, string)> SaveTrackFilesAsync(TrackRequestDto trackRequestDto)
    {
        var coverPath = trackRequestDto.Image != null
            ? await fileService.SaveFileAsync(trackRequestDto.Image, FileTypes.TrackCover)
            : null;

        var trackPath = await fileService.SaveFileAsync(trackRequestDto.TrackFile, FileTypes.Track);

        return (coverPath, trackPath);
    }

    private Func<Task>[] GetRollBackActions(string? coverPath, string trackPath)
    {
        return
        [
            string.IsNullOrEmpty(coverPath)
                ? () => Task.CompletedTask
                : () => Task.Run(() => fileService.DeleteFile(coverPath)),

            () => Task.Run(() => fileService.DeleteFile(trackPath))
        ];
    }
}
