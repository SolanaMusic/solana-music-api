using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.Playlist;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.PlaylistServices.PlaylistTrackService;

public class PlaylistTrackService(IBaseRepository<PlaylistTrack> baseRepository)
    : BaseService<PlaylistTrack>(baseRepository), IPlaylistTrackService;
