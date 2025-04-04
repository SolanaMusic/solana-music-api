using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.ArtistRepositories.ArtistAlbumRepository;

public class ArtistAlbumRepository(ApplicationDbContext context) : BaseRepository<ArtistAlbum>(context), IArtistAlbumRepository;
