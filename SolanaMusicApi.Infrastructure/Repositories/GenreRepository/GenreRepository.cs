using GenreEntity = SolanaMusicApi.Domain.Entities.Music.Genre;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.GenreRepository;

public class GenreRepository(ApplicationDbContext context) : BaseRepository<GenreEntity>(context), IGenreRepository;
