using System.Linq.Expressions;

namespace SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

public interface IBaseRepository<T>
{
    IQueryable<T> GetAll();
    Task<T> GetByIdAsync(long id);
    Task<T> AddAsync(T entity);
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
    Task<T> UpdateAsync(T entity);
    Task<T> UpdateAsync(long id, T entity);
    Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities);
    Task<T> DeleteAsync(long id);
    Task<T> DeleteAsync(Expression<Func<T, bool>> expression);
    Task<IEnumerable<T>> DeleteRangeAsync(IEnumerable<T> entities);

    Task BeginTransactionAsync();
    Task CommitTransactionAsync(params Func<Task>[]? rollbackActions);
    Task RollbackTransactionAsync(Exception ex, params Func<Task>[]? rollbackActions);
    Task ProcessRollBackActions(Func<Task>[]? rollbackActions);
}
