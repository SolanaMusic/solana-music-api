using SolanaMusicApi.Application;
using SolanaMusicApi.Domain.Entities.Music;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.TrackRepository;

public class TrackRepository : BaseRepository<Track>, ITrackRepository
{
    public TrackRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
