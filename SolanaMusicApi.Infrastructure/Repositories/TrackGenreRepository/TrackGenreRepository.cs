using SolanaMusicApi.Application;
using SolanaMusicApi.Domain.Entities.Music;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.TrackGenreRepository;

public class TrackGenreRepository : BaseRepository<TrackGenre>, ITrackGenreRepository
{
    public TrackGenreRepository(ApplicationDbContext context) : base(context) { }
}
