using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.Transaction;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.CurrencyService;

public class CurrencyService(IBaseRepository<Currency> baseRepository) : BaseService<Currency>(baseRepository), ICurrencyService;
