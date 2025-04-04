using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.ArtistRepositories.ArtistTrackRepository;

public class ArtistTrackRepository(ApplicationDbContext context) : BaseRepository<ArtistTrack>(context), IArtistTrackRepository;
