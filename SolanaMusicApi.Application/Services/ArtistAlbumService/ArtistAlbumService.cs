using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.ArtistAlbumService;

public class ArtistAlbumService : BaseService<ArtistAlbum>, IArtistAlbumService
{
    public ArtistAlbumService(IBaseRepository<ArtistAlbum> baseRepository) : base(baseRepository) { }
}
