using SolanaMusicApi.Domain.Entities.Transaction;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.CurrencyRepository;

public interface ICurrencyRepository : IBaseRepository<Currency>;
