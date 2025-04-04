using SolanaMusicApi.Domain.Entities.Playlist;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.PlaylistRepositories.PlaylistTrackRepository;

public class PlaylistTrackRepository(ApplicationDbContext context) : BaseRepository<PlaylistTrack>(context), IPlaylistTrackRepository;
