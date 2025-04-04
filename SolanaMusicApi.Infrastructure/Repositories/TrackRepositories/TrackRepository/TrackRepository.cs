using SolanaMusicApi.Domain.Entities.Music;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.TrackRepositories.TrackRepository;

public class TrackRepository(ApplicationDbContext dbContext) : BaseRepository<Track>(dbContext), ITrackRepository;
