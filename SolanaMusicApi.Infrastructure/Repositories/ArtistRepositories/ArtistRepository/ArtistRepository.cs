using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.ArtistRepositories.ArtistRepository;

public class ArtistRepository(ApplicationDbContext context) : BaseRepository<Artist>(context), IArtistRepository;
