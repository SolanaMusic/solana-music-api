using System.Linq.Expressions;
using SolanaMusicApi.Domain.DTO.Pagination;
using SolanaMusicApi.Domain.Enums;

namespace SolanaMusicApi.Domain.DTO.Dashboard;

public class DashboardResponsePaginationDto<T>(
    DashboardFilter request,
    IQueryable<T> data,
    Expression<Func<T, DateTime>> createdDateSelector)
    : ResponsePaginationDto<T>(request, ApplyTimeFilter(data, request.TimeFilter, createdDateSelector))
{
    private static IQueryable<T> ApplyTimeFilter(
        IQueryable<T> data,
        TimeFilter filter,
        Expression<Func<T, DateTime>> createdDateSelector)
    {
        var now = DateTime.UtcNow;
        var today = now.Date;

        DateTime start;
        DateTime end;

        switch (filter)
        {
            case TimeFilter.Today:
                start = today;
                end = today.AddDays(1);
                break;

            case TimeFilter.Week:
                var weekStart = today.AddDays(-((int)now.DayOfWeek == 0 ? 6 : (int)now.DayOfWeek - 1));
                start = weekStart;
                end = weekStart.AddDays(7);
                break;

            case TimeFilter.Month:
                start = new DateTime(now.Year, now.Month, 1);
                end = start.AddMonths(1);
                break;

            case TimeFilter.Year:
                start = new DateTime(now.Year, 1, 1);
                end = start.AddYears(1);
                break;

            case TimeFilter.AllTime:
                return data;

            default:
                throw new ArgumentOutOfRangeException(nameof(filter), filter, "Invalid Time Filter");
        }
        
        var param = createdDateSelector.Parameters[0];
        var body = Expression.AndAlso(
            Expression.GreaterThanOrEqual(createdDateSelector.Body, Expression.Constant(start)),
            Expression.LessThan(createdDateSelector.Body, Expression.Constant(end))
        );

        var lambda = Expression.Lambda<Func<T, bool>>(body, param);
        return data.Where(lambda);
    }
}