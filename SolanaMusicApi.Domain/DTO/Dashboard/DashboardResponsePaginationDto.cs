using SolanaMusicApi.Domain.DTO.Pagination;
using SolanaMusicApi.Domain.Entities;
using SolanaMusicApi.Domain.Enums;

namespace SolanaMusicApi.Domain.DTO.Dashboard;

public class DashboardResponsePaginationDto<T>(DashboardFilter request, IQueryable<T> data)
    : ResponsePaginationDto<T>(request, ApplyTimeFilter(data, request.TimeFilter)) where T : BaseEntity
{
    private static IQueryable<T> ApplyTimeFilter(IQueryable<T> data, TimeFilter filter)
    {
        var now = DateTime.UtcNow;

        return filter switch
        {
            TimeFilter.Today => data.Where(x => x.CreatedDate.Date == now.Date),
            TimeFilter.Week => data.Where(x => x.CreatedDate >= now.Date.AddDays(-((int)now.DayOfWeek == 0 ? 6 : (int)now.DayOfWeek - 1))),
            TimeFilter.Month => data.Where(x => x.CreatedDate >= new DateTime(now.Year, now.Month, 1)),
            TimeFilter.Year => data.Where(x => x.CreatedDate >= new DateTime(now.Year, 1, 1)),
            TimeFilter.AllTime => data,
            _ => throw new ArgumentOutOfRangeException(nameof(filter), filter, "Invalid Time Filter")
        };
    }
}