namespace SolanaMusicApi.Application.Services.BaseService;

public interface IBaseService<T>
{
    IQueryable<T> GetAll();
    Task<T> GetByIdAsync(long id);
    Task<T> AddAsync(T entity);
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
    Task<T> UpdateAsync(long id, T entity);
    Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities);
    Task<bool> DeleteAsync(long id);
    Task DeleteRangeAsync(IEnumerable<T> entities);

    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync(Exception ex);
}
