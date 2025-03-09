using SolanaMusicApi.Application;
using SolanaMusicApi.Domain.Entities.Playlist;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.PlaylistRespositories.PlaylistTrackRespository;

public class PlaylistTrackRespository : BaseRepository<PlaylistTrack>, IPlaylistTrackRespository
{
    public PlaylistTrackRespository(ApplicationDbContext context) : base(context) { }
}
