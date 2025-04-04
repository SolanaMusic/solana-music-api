using SolanaMusicApi.Domain.Entities.Transaction;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.TransactionRepository;

public interface ITransactionRepository : IBaseRepository<Transaction>;
