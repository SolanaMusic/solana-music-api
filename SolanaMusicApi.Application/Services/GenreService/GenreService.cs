using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.Music;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.GenreService;

public class GenreService : BaseService<Genre>, IGenreService
{
    public GenreService(IBaseRepository<Genre> baseRepository) : base(baseRepository) { }
}
