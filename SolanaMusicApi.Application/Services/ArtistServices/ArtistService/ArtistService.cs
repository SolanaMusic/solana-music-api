using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.ArtistServices.ArtistService;

public class ArtistService : BaseService<Artist>, IArtistService
{
    public ArtistService(IBaseRepository<Artist> baseRepository) : base(baseRepository) { }
}
