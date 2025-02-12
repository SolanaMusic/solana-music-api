namespace SolanaMusicApi.Application.Services.BaseService;

public interface IBaseService<T>
{
    IQueryable<T> GetAll();
    Task<T> GetByIdAsync(int id);
    Task<T> Add(T entity);
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task UpdateRangeAsync(IEnumerable<T> entities);
    Task DeleteAsync(int id);
    Task DeleteRangeAsync(IEnumerable<T> entities);

    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync(Exception ex);
}
