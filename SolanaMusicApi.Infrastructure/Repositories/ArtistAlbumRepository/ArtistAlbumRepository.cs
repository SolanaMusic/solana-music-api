using SolanaMusicApi.Application;
using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.ArtistAlbumRepository;

public class ArtistAlbumRepository : BaseRepository<ArtistAlbum>, IArtistAlbumRepository
{
    public ArtistAlbumRepository(ApplicationDbContext context) : base(context) { }
}
