using SolanaMusicApi.Domain.Entities;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.BaseService;

public class BaseService<T>(IBaseRepository<T> baseRepository) : IBaseService<T> where T : BaseEntity
{

    public IQueryable<T> GetAll() => baseRepository.GetAll();

    public async Task<T> GetByIdAsync(long id) => await baseRepository.GetByIdAsync(id);

    public async Task<T> AddAsync(T entity) => await baseRepository.AddAsync(entity);

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity) => await baseRepository.AddRangeAsync(entity);

    public async Task<T> UpdateAsync(long id, T entity) => await baseRepository.UpdateAsync(id, entity);

    public async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities) => await baseRepository.UpdateRangeAsync(entities);

    public async Task<T> DeleteAsync(long id) => await baseRepository.DeleteAsync(id);

    public async Task DeleteRangeAsync(IEnumerable<T> entities) => await baseRepository.DeleteRangeAsync(entities);

    public async Task BeginTransactionAsync() => await baseRepository.BeginTransactionAsync();

    public async Task CommitTransactionAsync(params Func<Task>[]? rollbackActions) => await baseRepository.CommitTransactionAsync(rollbackActions);

    public async Task RollbackTransactionAsync(Exception ex, params Func<Task>[]? rollbackActions) => 
        await baseRepository.RollbackTransactionAsync(ex, rollbackActions);
}
