using SolanaMusicApi.Domain.Entities.General;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.WhitelistRepository;

public interface IWhitelistRepository : IBaseRepository<Whitelist>;