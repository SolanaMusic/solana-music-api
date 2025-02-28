using GenreEntity = SolanaMusicApi.Domain.Entities.Music.Genre;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.Genre;

public interface IGenreRepository : IBaseRepository<GenreEntity>;
