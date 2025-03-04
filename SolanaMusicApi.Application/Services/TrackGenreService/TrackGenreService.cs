using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.Music;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.TrackGenreService;

public class TrackGenreService : BaseService<TrackGenre>, ITrackGenreService
{
    public TrackGenreService(IBaseRepository<TrackGenre> baseRepository) : base(baseRepository) { }
}
