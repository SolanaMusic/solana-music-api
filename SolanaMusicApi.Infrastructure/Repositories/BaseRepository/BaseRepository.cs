using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Domain.Entities;
using System.Linq.Expressions;

namespace SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;
    private readonly DbSet<T> _dbSet;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public IQueryable<T> GetAll()
    {
        return _dbSet
            .AsQueryable()
            .AsNoTracking();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
    {
        var response = await GetAll()
            .SingleOrDefaultAsync(expression);

        if (response == null)
            throw new NullReferenceException($"{typeof(T).Name} not found.");

        return response;
    }

    public async Task<T> GetByIdAsync(long id)
    {
        var response = await GetAll()
            .SingleOrDefaultAsync(x => x.Id == id);

        if (response == null)
            throw new NullReferenceException($"{typeof(T).Name} not found.");

        return response;
    }

    public async Task<T> AddAsync(T entity)
    {
        var date = DateTime.UtcNow;
        entity.CreatedDate = date;
        entity.UpdatedDate = date;

        _dbSet.Add(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        var date = DateTime.UtcNow;
        var entityList = entities.ToList();

        foreach (var entity in entityList)
        {
            entity.CreatedDate = date;
            entity.UpdatedDate = date;
        }

        await _dbSet.AddRangeAsync(entityList);
        await _context.SaveChangesAsync();

        return entityList;
    }

    public async Task<T> UpdateAsync(long id, T entity)
    {
        var existingEntity = await GetByIdAsyncTraking(id);
        entity.Id = id;
        entity.UpdatedDate = DateTime.UtcNow;
        entity.CreatedDate = existingEntity.CreatedDate;

        _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();

        return existingEntity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        entity.UpdatedDate = DateTime.UtcNow;
        var entry = _context.Entry(entity);
        if (entry.State == EntityState.Detached)
            _context.Set<T>().Attach(entity);

        entry.State = EntityState.Modified;
        _context.Entry(entity).CurrentValues.SetValues(entity);

        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities)
    {
        var date = DateTime.UtcNow;
        var entityList = entities.ToList();

        foreach (var entity in entityList)
            entity.UpdatedDate = date;

        _dbSet.UpdateRange(entityList);
        await _context.SaveChangesAsync();
        return entityList;
    }

    public async Task<T> DeleteAsync(long id)
    {
        var entity = await GetByIdAsyncTraking(id);
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<T> DeleteAsync(Expression<Func<T, bool>> expression)
    {
        var entity = await GetByIdAsyncTraking(expression);
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<IEnumerable<T>> DeleteRangeAsync(IEnumerable<T> entities)
    {
        var deleteRangeAsync = entities.ToList();
        _context.RemoveRange(deleteRangeAsync);
        await _context.SaveChangesAsync();
        return deleteRangeAsync;
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync(params Func<Task>[]? rollbackActions)
    {
        if (_transaction == null)
            throw new InvalidOperationException("Transaction has not been started");

        try
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await RollbackTransactionAsync(ex, rollbackActions);
        }
        finally
        {
            await _transaction.DisposeAsync();
        }
    }

    public async Task RollbackTransactionAsync(Exception ex, params Func<Task>[]? rollbackActions)
    {
        if (_transaction == null)
            return;

        try
        {
            await _transaction.RollbackAsync();
            throw new Exception(ex.Message);
        }
        catch (Exception rollbackEx)
        {
            throw new Exception(rollbackEx.Message);
        }
        finally
        {
            await ProcessRollBackActions(rollbackActions);
            await _transaction.DisposeAsync();
        }
    }

    private async Task<T> GetByIdAsyncTraking(long id)
    {
        var response = await _dbSet
            .AsQueryable()
            .SingleOrDefaultAsync(x => x.Id == id);

        if (response == null)
            throw new NullReferenceException($"{typeof(T).Name} not found.");

        return response;
    }

    private async Task<T> GetByIdAsyncTraking(Expression<Func<T, bool>> expression)
    {
        var response = await _dbSet
            .AsQueryable()
            .SingleOrDefaultAsync(expression);

        if (response == null)
            throw new NullReferenceException($"{typeof(T).Name} not found.");

        return response;
    }

    public async Task ProcessRollBackActions(Func<Task>[]? rollbackActions)
    {
        if (rollbackActions != null)
            await Task.WhenAll(rollbackActions.Select(action => action()));
    }
}
