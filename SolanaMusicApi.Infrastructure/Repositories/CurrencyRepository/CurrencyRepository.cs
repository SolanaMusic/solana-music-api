using SolanaMusicApi.Domain.Entities.Transaction;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.CurrencyRepository;

public class CurrencyRepository(ApplicationDbContext context) : BaseRepository<Currency>(context), ICurrencyRepository;
