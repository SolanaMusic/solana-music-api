using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.Music;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.TracksService;

public class TracksService : BaseService<Track>, ITracksService
{
    public TracksService(IBaseRepository<Track> baseRepository) : base(baseRepository) { }
}
