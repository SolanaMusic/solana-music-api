using SolanaMusicApi.Domain.Entities.General;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.WhitelistRepository;

public class WhitelistRepository(ApplicationDbContext context) : BaseRepository<Whitelist>(context), IWhitelistRepository;