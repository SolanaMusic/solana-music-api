using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.Transaction;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.CurrencyService;

public class CurrencyService : BaseService<Currency>, ICurrencyService
{
    public CurrencyService(IBaseRepository<Currency> baseRepository) : base(baseRepository) { }
}
