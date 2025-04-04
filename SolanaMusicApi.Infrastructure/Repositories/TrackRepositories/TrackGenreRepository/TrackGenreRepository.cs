using SolanaMusicApi.Domain.Entities.Music;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.TrackRepositories.TrackGenreRepository;

public class TrackGenreRepository(ApplicationDbContext context) : BaseRepository<TrackGenre>(context), ITrackGenreRepository;
