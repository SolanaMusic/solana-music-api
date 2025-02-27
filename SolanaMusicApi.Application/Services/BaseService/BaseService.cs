using SolanaMusicApi.Domain.Entities;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.BaseService;

public class BaseService<T> : IBaseService<T> where T : BaseEntity
{
    protected readonly IBaseRepository<T> _baseRepository;

    public BaseService(IBaseRepository<T> baseRepository) => _baseRepository = baseRepository;

    public IQueryable<T> GetAll() => _baseRepository.GetAll();

    public async Task<T> GetByIdAsync(long id) => await _baseRepository.GetByIdAsync(id);

    public async Task<T> AddAsync(T entity) => await _baseRepository.AddAsync(entity);

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity) => await _baseRepository.AddRangeAsync(entity);

    public async Task<T> UpdateAsync(T entity) => await _baseRepository.UpdateAsync(entity);

    public async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities) => await _baseRepository.UpdateRangeAsync(entities);

    public async Task<bool> DeleteAsync(long id) => await _baseRepository.DeleteAsync(id);

    public async Task DeleteRangeAsync(IEnumerable<T> entities) => await _baseRepository.DeleteRangeAsync(entities);

    public async Task BeginTransactionAsync() => await _baseRepository.BeginTransactionAsync();

    public async Task CommitTransactionAsync() => await _baseRepository.CommitTransactionAsync();

    public async Task RollbackTransactionAsync(Exception ex) => await _baseRepository.RollbackTransactionAsync(ex);
}
