using SolanaMusicApi.Domain.Entities;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.BaseService;

public class BaseService<T>(IBaseRepository<T> baseRepository) : IBaseService<T> where T : BaseEntity
{
    public IQueryable<T> GetAll()
    {
        return baseRepository.GetAll();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await baseRepository.GetByIdAsync(id);
    }

    public async Task<T> Add(T entity)
    {
        return await baseRepository.Add(entity);
    }

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity)
    {
        return await baseRepository.AddRangeAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        await baseRepository.UpdateAsync(entity);
    }

    public async Task UpdateRangeAsync(IEnumerable<T> entities)
    {
        await baseRepository.UpdateRangeAsync(entities);
    }

    public async Task DeleteAsync(int id)
    {
        await baseRepository.DeleteAsync(id);
    }

    public async Task DeleteRangeAsync(IEnumerable<T> entities)
    {
        await baseRepository.DeleteRangeAsync(entities);
    }

    public async Task BeginTransactionAsync()
    {
        await baseRepository.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await baseRepository.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync(Exception ex)
    {
        await baseRepository.RollbackTransactionAsync(ex);
    }
}
