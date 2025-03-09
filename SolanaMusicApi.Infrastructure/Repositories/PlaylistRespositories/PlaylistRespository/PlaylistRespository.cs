using SolanaMusicApi.Application;
using SolanaMusicApi.Domain.Entities.Playlist;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.PlaylistRespositories.PlaylistRespository;

public class PlaylistRespository : BaseRepository<Playlist>, IPlaylistRespository
{
    public PlaylistRespository(ApplicationDbContext context) : base(context) { }
}
