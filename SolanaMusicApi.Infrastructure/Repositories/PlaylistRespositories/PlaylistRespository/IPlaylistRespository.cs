using SolanaMusicApi.Domain.Entities.Playlist;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.PlaylistRespositories.PlaylistRespository;

public interface IPlaylistRespository : IBaseRepository<Playlist>;
