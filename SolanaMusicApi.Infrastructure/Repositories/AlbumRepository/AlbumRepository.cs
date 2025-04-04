using SolanaMusicApi.Domain.Entities.Music;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.AlbumRepository;

public class AlbumRepository(ApplicationDbContext context) : BaseRepository<Album>(context), IAlbumRepository;
