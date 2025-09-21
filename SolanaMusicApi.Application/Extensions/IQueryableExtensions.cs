using System.Linq.Expressions;
using SolanaMusicApi.Domain.DTO.Sorting;
using SolanaMusicApi.Domain.Enums;

namespace SolanaMusicApi.Application.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, RequestSortingDto sorting)
    {
        if (string.IsNullOrWhiteSpace(sorting.SortColumn))
            return query;

        var parameter = Expression.Parameter(typeof(T), "x");
        
        var property = sorting.SortColumn
            .Split('.')
            .Aggregate((Expression)parameter, Expression.PropertyOrField);

        var lambda = Expression.Lambda(property, parameter);

        var methodName = sorting.SortDirection == SortDirection.Desc
            ? nameof(Queryable.OrderByDescending)
            : nameof(Queryable.OrderBy);

        var call = Expression.Call(
            typeof(Queryable),
            methodName,
            [typeof(T), property.Type],
            query.Expression,
            Expression.Quote(lambda));

        return query.Provider.CreateQuery<T>(call);
    }
}
