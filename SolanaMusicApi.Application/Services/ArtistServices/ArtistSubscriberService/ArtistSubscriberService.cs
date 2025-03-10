using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.ArtistServices.ArtistSubscriberService;

public class ArtistSubscriberService : BaseService<ArtistSubscriber>, IArtistSubscriberService
{
    public ArtistSubscriberService(IBaseRepository<ArtistSubscriber> baseRepository) : base(baseRepository) { }
}
