using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.ArtistRepositories.ArtistSubscriberRepository;

public class ArtistSubscriberRepository(ApplicationDbContext context)
    : BaseRepository<ArtistSubscriber>(context), IArtistSubscriberRepository;
