namespace SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

public interface IBaseRepository<T>
{
    IQueryable<T> GetAll();
    Task<T> GetByIdAsync(long id);
    Task<T> AddAsync(T entity);
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task UpdateRangeAsync(IEnumerable<T> entities);
    Task DeleteAsync(long id);
    Task DeleteRangeAsync(IEnumerable<T> entities);

    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync(Exception ex);
}
