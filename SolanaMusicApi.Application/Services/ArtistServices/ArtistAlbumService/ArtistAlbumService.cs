using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.ArtistServices.ArtistAlbumService;

public class ArtistAlbumService : BaseService<ArtistAlbum>, IArtistAlbumService
{
    public ArtistAlbumService(IBaseRepository<ArtistAlbum> baseRepository) : base(baseRepository) { }
}
