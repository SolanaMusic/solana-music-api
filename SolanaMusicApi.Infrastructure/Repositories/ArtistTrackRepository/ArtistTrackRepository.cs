using SolanaMusicApi.Application;
using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.ArtistTrackRepository;

public class ArtistTrackRepository : BaseRepository<ArtistTrack>, IArtistTrackRepository
{
    public ArtistTrackRepository(ApplicationDbContext context) : base(context) { }
}
