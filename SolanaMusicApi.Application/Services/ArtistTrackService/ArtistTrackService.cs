using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.ArtistTrackService;

public class ArtistTrackService : BaseService<ArtistTrack>, IArtistTrackService
{
    public ArtistTrackService(IBaseRepository<ArtistTrack> baseRepository) : base(baseRepository) { }
}
