using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.ArtistServices.ArtistSubscriberService;

public class ArtistSubscriberService(IBaseRepository<ArtistSubscriber> baseRepository)
    : BaseService<ArtistSubscriber>(baseRepository), IArtistSubscriberService;
