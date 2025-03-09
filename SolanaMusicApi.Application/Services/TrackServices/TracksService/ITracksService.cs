using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.DTO.Track;
using SolanaMusicApi.Domain.Entities.Music;

namespace SolanaMusicApi.Application.Services.TrackServices.TracksService;

public interface ITracksService : IBaseService<Track>
{
    Task<List<TrackResponseDto>> GetTracksAsync();
    Task<TrackResponseDto> GetTrackAsync(long id);
    Task<FileStream> GetTrackFileStreamAsync(long id);
    Task<TrackResponseDto> CreateTrackAsync(TrackRequestDto trackRequestDto);
    Task<TrackResponseDto> UpdateTrackAsync(long id, TrackRequestDto trackRequestDto);
    Task<TrackResponseDto> DeleteTrackAsync(long id);
}
