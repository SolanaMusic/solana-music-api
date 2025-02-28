using GenreEntity = SolanaMusicApi.Domain.Entities.Music.Genre;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;
using SolanaMusicApi.Application;

namespace SolanaMusicApi.Infrastructure.Repositories.Genre;

public class GenreRepository : BaseRepository<GenreEntity>, IGenreRepository
{
    public GenreRepository(ApplicationDbContext context) : base(context) { }
}
