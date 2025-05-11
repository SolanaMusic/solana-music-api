using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.Music;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.GenreService;

public class GenreService(IBaseRepository<Genre> baseRepository) : BaseService<Genre>(baseRepository), IGenreService;
