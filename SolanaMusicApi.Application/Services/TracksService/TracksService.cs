using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Services.ArtistService;
using SolanaMusicApi.Application.Services.ArtistTrackService;
using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Application.Services.FileService;
using SolanaMusicApi.Application.Services.GenreService;
using SolanaMusicApi.Application.Services.TrackGenreService;
using SolanaMusicApi.Domain.DTO.Track;
using SolanaMusicApi.Domain.Entities.Music;
using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Domain.Enums.File;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.TracksService;

public class TracksService : BaseService<Track>, ITracksService
{
    private IMapper _mapper;
    private readonly IFileService _fileService;
    private readonly IGenreService _genreService;
    private readonly ITrackGenreService _trackGenreService;
    private readonly IArtistService _artistService;
    private readonly IArtistTrackService _artistTrackService;

    public TracksService(IBaseRepository<Track> baseRepository, ITrackGenreService trackGenreService, IFileService fileService, 
        IGenreService genreService, IArtistService artistService, IArtistTrackService artistTrackService, IMapper mapper) : base(baseRepository)
    {
        _mapper = mapper;
        _fileService = fileService;
        _genreService = genreService;
        _trackGenreService = trackGenreService;
        _artistService = artistService;
        _artistTrackService = artistTrackService;
    }

    public void MapProperties(Track track)
    {
        if (string.IsNullOrEmpty(track.ImageUrl) && track.Album?.ImageUrl != null)
            track.ImageUrl = track.Album.ImageUrl;

        if (!track.ArtistTracks.Any() && track.Album?.Artists.Any() == true)
        {
            var artistTracksList = track.ArtistTracks as List<ArtistTrack>;

            if (artistTracksList != null)
                artistTracksList.AddRange(track.Album.Artists.Select(artist => new ArtistTrack { Track = track, Artist = artist }));
        }
    }

    public async Task<List<TrackResponseDto>> GetTracksAsync()
    {
        var tracks = await GetTracksQueryAsync();
        var response = await tracks.ToListAsync();
       
        foreach (var track in response)
            MapProperties(track);

        return _mapper.Map<List<TrackResponseDto>>(response);
    }

    public async Task<TrackResponseDto> GetTrackAsync(long id)
    {
        var trackQuery = await GetTracksQueryAsync();
        var track = await trackQuery.FirstOrDefaultAsync(t => t.Id == id) ?? throw new KeyNotFoundException("Track not found");
        MapProperties(track);

        return _mapper.Map<TrackResponseDto>(track);
    }

    public async Task<FileStream> GetTrackFileStreamAsync(long id)
    {
        var response = await GetByIdAsync(id);
        return new FileStream(response.FileUrl, FileMode.Open, FileAccess.Read, FileShare.Read);
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
            return _mapper.Map<TrackResponseDto>(response);
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

            var updated = await UpdateAsync(track);
            await ProcessRollBackActions(GetRollBackActions(imageSnapshot, fileSnapshot));
            await CommitTransactionAsync(GetRollBackActions(coverPath, trackPath));

            var response = await GetTrackAsync(id);
            return _mapper.Map<TrackResponseDto>(response);
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

        return _mapper.Map<TrackResponseDto>(response);
    }

    private async Task<IQueryable<Track>> GetTracksQueryAsync()
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
                .Include(t => t.Album!.Artists)
                    .ThenInclude(ar => ar.Country);
        }

        return trackQuery;
    }

    private Track MapTrack(TrackRequestDto trackRequestDto, string trackFile, string? coverFile)
    {
        var track = _mapper.Map<Track>(trackRequestDto);
        track.FileUrl = trackFile;
        track.ImageUrl = coverFile;
        track.Duration = _fileService.GetAudioDuration(trackRequestDto.TrackFile);

        return track;
    }

    private async Task MapTrackAsync(Track track, TrackRequestDto trackRequestDto, string trackFile, string? coverFile)
    {
        track.Title = trackRequestDto.Title;
        track.AlbumId = trackRequestDto.AlbumId;
        track.FileUrl = trackFile;
        track.ImageUrl = coverFile;
        track.Duration = _fileService.GetAudioDuration(trackRequestDto.TrackFile);
        
        if (track.TrackGenres.Count() != trackRequestDto.GenreIds.Count() || track.ArtistTracks.Count() != trackRequestDto.ArtistIds.Count())
            await UpdateLinkedDataAsync(track, trackRequestDto);
    }

    private async Task<Track> UpdateLinkedDataAsync(Track track, TrackRequestDto trackRequestDto)
    {
        var existingGenres = track.TrackGenres.Select(x => x.GenreId);
        var genresToAdd = trackRequestDto.GenreIds.Except(existingGenres).ToList();
        var genresToRemove = existingGenres.Except(trackRequestDto.GenreIds).ToList();

        var existingArtists = track.ArtistTracks.Select(x => x.ArtistId);
        var artistsToAdd = trackRequestDto.ArtistIds.Except(existingArtists).ToList();
        var artistsToRemove = existingArtists.Except(trackRequestDto.ArtistIds).ToList();

        if (genresToRemove.Any())
        {
            var genresToDelete = track.TrackGenres.Where(x => genresToRemove.Contains(x.GenreId)).ToList();
            await _trackGenreService.DeleteRangeAsync(genresToDelete);
        }

        if (artistsToRemove.Any())
        {
            var artistsToDelete = track.ArtistTracks.Where(x => artistsToRemove.Contains(x.ArtistId)).ToList();
            await _artistTrackService.DeleteRangeAsync(artistsToDelete);
        }

        if (genresToAdd.Any() || artistsToAdd.Any())
        {
            trackRequestDto.GenreIds = genresToAdd;
            trackRequestDto.ArtistIds = artistsToAdd;
            await InsertLinkedDataAsync(track, trackRequestDto);
        }

        return track;
    }

    private async Task InsertLinkedDataAsync(Track track, TrackRequestDto trackRequestDto)
    {
        var (genres, artists) = GetLinkedData(track, trackRequestDto);
        await _trackGenreService.AddRangeAsync(genres);             
        await _artistTrackService.AddRangeAsync(artists);
    }

    private (IQueryable<TrackGenre>, IQueryable<ArtistTrack>) GetLinkedData(Track track, TrackRequestDto trackRequestDto)
    {
        var genres = _genreService
            .GetAll()
            .Where(x => trackRequestDto.GenreIds.Contains(x.Id));

        var artists = _artistService
            .GetAll()
            .Where(x => trackRequestDto.ArtistIds!.Contains(x.Id));

        var trackGenres = genres.Select(genre => new TrackGenre { TrackId = track.Id, GenreId = genre.Id });
        var artistTracks = artists.Select(artist => new ArtistTrack { ArtistId = artist.Id, TrackId = track.Id });

        return (trackGenres, artistTracks);
    }

    private async Task<(string?, string)> SaveTrackFilesAsync(TrackRequestDto trackRequestDto)
    {
        var coverPath = trackRequestDto.Image != null
            ? await _fileService.SaveFileAsync(trackRequestDto.Image, FileTypes.TrackCover)
            : null;

        var trackPath = await _fileService.SaveFileAsync(trackRequestDto.TrackFile, FileTypes.Track);

        return (coverPath, trackPath);
    }

    private Func<Task>[] GetRollBackActions(string? coverPath, string trackPath)
    {
        return
        [
            string.IsNullOrEmpty(coverPath)
                ? () => Task.CompletedTask
                : () => Task.Run(() => _fileService.DeleteFile(coverPath)),

            () => Task.Run(() => _fileService.DeleteFile(trackPath))
        ];
    }
}
