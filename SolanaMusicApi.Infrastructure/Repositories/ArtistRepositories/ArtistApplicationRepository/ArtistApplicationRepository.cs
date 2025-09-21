using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.ArtistRepositories.ArtistApplicationRepository;

public class ArtistApplicationRepository(ApplicationDbContext context)
    : BaseRepository<ArtistApplication>(context), IArtistApplicationRepository;