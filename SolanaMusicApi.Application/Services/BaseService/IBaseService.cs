﻿using System.Linq.Expressions;

namespace SolanaMusicApi.Application.Services.BaseService;

public interface IBaseService<T>
{
    IQueryable<T> GetAll();
    Task<T> GetAsync(Expression<Func<T, bool>> expression);
    Task<T> GetByIdAsync(long id);
    Task<T> AddAsync(T entity);
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
    Task<T> UpdateAsync(long id, T entity);
    Task<T> UpdateAsync(T entity);
    Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities);
    Task<T> DeleteAsync(long id);
    Task<T> DeleteAsync(Expression<Func<T, bool>> expression);
    Task DeleteRangeAsync(IEnumerable<T> entities);

    Task BeginTransactionAsync();
    Task CommitTransactionAsync(params Func<Task>[]? rollbackActions);
    Task RollbackTransactionAsync(Exception ex, params Func<Task>[]? rollbackActions);
    Task ProcessRollBackActions(Func<Task>[]? rollbackActions);
}
