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

    public async Task<TrackResponseDto> GetTrackAsync(long id)
    {
        var track = await GetAll()
            .Include(t => t.ArtistTracks)
                .ThenInclude(x => x.Artist)
                    .ThenInclude(ar => ar.Country)

            .Include(t => t.Album)
                .ThenInclude(al => al.Artists)
                    .ThenInclude(ar => ar.Country)

            .Include(t => t.TrackGenres)
                .ThenInclude(x => x.Genre)

            .FirstOrDefaultAsync(t => t.Id == id) ?? throw new KeyNotFoundException("Track not found");

        if (string.IsNullOrEmpty(track.ImageUrl) && track.Album?.ImageUrl != null)
            track.ImageUrl = track.Album.ImageUrl;

        if (!track.ArtistTracks.Any() && track.Album?.Artists.Any() == true)
        {
            var artistTracksList = track.ArtistTracks as List<ArtistTrack>;

            if (artistTracksList != null)
                artistTracksList.AddRange(track.Album.Artists.Select(artist => new ArtistTrack { Track = track, Artist = artist }));
        }

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
        var (coverPath, trackPath) = await SaveTrackFiles(trackRequestDto);

        try
        {
            var track = MapTrack(trackRequestDto, trackPath, coverPath);
            var added = await AddAsync(track);
            await InsertIntermediateTables(trackRequestDto, added);
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

    public async Task<TrackResponseDto> DeleteTrackAsync(long id)
    {
        var response = await DeleteAsync(id);
        await Task.WhenAll(GetRollBackActions(response.ImageUrl ?? string.Empty, response.FileUrl).Select(action => action()));

        return _mapper.Map<TrackResponseDto>(response);
    }

    private Track MapTrack(TrackRequestDto trackRequestDto, string trackFile, string coverFile)
    {
        var track = _mapper.Map<Track>(trackRequestDto);

        track.FileUrl = trackFile;
        track.ImageUrl = coverFile;
        track.Duration = _fileService.GetAudioDuration(trackRequestDto.TrackFile);

        return track;
    }

    private async Task InsertIntermediateTables(TrackRequestDto trackRequestDto, Track track)
    {
        var genres = await _genreService
            .GetAll()
            .Where(x => trackRequestDto.GenreIds.Contains(x.Id))
            .ToListAsync();

        var trackGenres = genres.Select(genre => new TrackGenre { TrackId = track.Id, GenreId = genre.Id });
        await _trackGenreService.AddRangeAsync(trackGenres);

        if (trackRequestDto.ArtistIds == null)
            return;

        var artists = await _artistService
            .GetAll()
            .Where(x => trackRequestDto.ArtistIds!.Contains(x.Id))
            .ToListAsync();
        
        var artistTracks = artists.Select(artist => new ArtistTrack { ArtistId = artist.Id, TrackId = track.Id });      
        await _artistTrackService.AddRangeAsync(artistTracks);
    }

    private async Task<(string, string)> SaveTrackFiles(TrackRequestDto trackRequestDto)
    {
        var coverPath = trackRequestDto.Image != null
            ? await _fileService.SaveFileAsync(trackRequestDto.Image, FileTypes.TrackCover)
            : string.Empty;

        var trackPath = await _fileService.SaveFileAsync(trackRequestDto.TrackFile, FileTypes.Track);

        return (coverPath, trackPath);
    }

    private Func<Task>[] GetRollBackActions(string coverPath, string trackPath)
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
