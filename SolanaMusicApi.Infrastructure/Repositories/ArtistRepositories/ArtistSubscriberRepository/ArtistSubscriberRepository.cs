using SolanaMusicApi.Application;
using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.ArtistRepositories.ArtistSubscriberRepository;

public class ArtistSubscriberRepository : BaseRepository<ArtistSubscriber>, IArtistSubscriberRepository
{
    public ArtistSubscriberRepository(ApplicationDbContext context) : base(context) { }
}
