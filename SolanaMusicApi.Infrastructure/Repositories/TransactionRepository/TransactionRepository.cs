using SolanaMusicApi.Domain.Entities.Transaction;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.TransactionRepository;

public class TransactionRepository(ApplicationDbContext context) : BaseRepository<Transaction>(context), ITransactionRepository;
