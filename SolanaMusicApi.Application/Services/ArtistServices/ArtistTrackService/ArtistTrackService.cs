using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.ArtistServices.ArtistTrackService;

public class ArtistTrackService(IBaseRepository<ArtistTrack> baseRepository) : BaseService<ArtistTrack>(baseRepository), IArtistTrackService;
