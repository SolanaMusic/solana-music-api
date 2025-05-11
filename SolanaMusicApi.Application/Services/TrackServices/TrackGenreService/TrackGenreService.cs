using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.Music;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.TrackServices.TrackGenreService;

public class TrackGenreService(IBaseRepository<TrackGenre> baseRepository) : BaseService<TrackGenre>(baseRepository), ITrackGenreService;
