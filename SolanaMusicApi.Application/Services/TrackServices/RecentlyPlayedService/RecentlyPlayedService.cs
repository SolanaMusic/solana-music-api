using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.General;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.TrackServices.RecentlyPlayedService;

public class RecentlyPlayedService(IBaseRepository<RecentlyPlayed> baseRepository) : BaseService<RecentlyPlayed>(baseRepository), IRecentlyPlayedService;