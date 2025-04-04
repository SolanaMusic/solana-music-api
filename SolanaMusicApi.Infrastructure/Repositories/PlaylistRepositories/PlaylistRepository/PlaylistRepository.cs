using SolanaMusicApi.Domain.Entities.Playlist;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.PlaylistRepositories.PlaylistRepository;

public class PlaylistRepository(ApplicationDbContext context) : BaseRepository<Playlist>(context), IPlaylistRepository;
