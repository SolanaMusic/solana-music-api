using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application;
using SolanaMusicApi.Domain.Entities;

namespace SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;
    private readonly DbSet<T> dbSet;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        dbSet = _context.Set<T>();
    }

    public IQueryable<T> GetAll()
    {
        return dbSet
            .AsQueryable()
            .AsNoTracking();
    }

    public async Task<T> GetByIdAsync(long id)
    {
        var response = await dbSet
            .AsQueryable()
            .AsNoTracking()
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

        dbSet.Add(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        var date = DateTime.UtcNow;
        foreach (var entity in entities)
        {
            entity.CreatedDate = date;
            entity.UpdatedDate = date;
        }

        await dbSet.AddRangeAsync(entities);
        await _context.SaveChangesAsync();

        return entities;
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

    public async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities)
    {
        var date = DateTime.UtcNow;
        foreach (var entity in entities)
            entity.UpdatedDate = date;

        dbSet.UpdateRange(entities);
        await _context.SaveChangesAsync();
        return entities;
    }

    public async Task<T> DeleteAsync(long id)
    {
        var entity = await GetByIdAsyncTraking(id);
        dbSet.Remove(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteRangeAsync(IEnumerable<T> entities)
    {
        _context.RemoveRange(entities);
        await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
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
            await RollbackTransactionAsync(ex);
        }
        finally
        {
            await _transaction.DisposeAsync();
        }
    }

    public async Task RollbackTransactionAsync(Exception ex)
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
            await _transaction.DisposeAsync();
        }
    }

    private async Task<T> GetByIdAsyncTraking(long id)
    {
        var response = await dbSet
            .AsQueryable()
            .SingleOrDefaultAsync(x => x.Id == id);

        if (response == null)
            throw new NullReferenceException($"{typeof(T).Name} not found.");

        return response;
    }
}
