using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.General;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.WhitelistService;

public class WhitelistService(IBaseRepository<Whitelist> baseRepository) : BaseService<Whitelist>(baseRepository), IWhitelistService;