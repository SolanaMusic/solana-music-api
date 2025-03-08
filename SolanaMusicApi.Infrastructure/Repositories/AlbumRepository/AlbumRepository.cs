using SolanaMusicApi.Application;
using SolanaMusicApi.Domain.Entities.Music;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.AlbumRepository;

public class AlbumRepository : BaseRepository<Album>, IAlbumRepository
{
    public AlbumRepository(ApplicationDbContext context) : base(context) { }
}
