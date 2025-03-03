using SolanaMusicApi.Application;
using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.ArtistRepository;

public class ArtistRepository : BaseRepository<Artist>, IArtistRepository
{
    public ArtistRepository(ApplicationDbContext context) : base(context) { }
}
