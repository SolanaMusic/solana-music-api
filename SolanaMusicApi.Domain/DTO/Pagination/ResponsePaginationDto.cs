using SolanaMusicApi.Domain.Entities;

namespace SolanaMusicApi.Domain.DTO.Pagination;

public class ResponsePaginationDto<T> where T : BaseEntity
{
    public ResponsePaginationDto(RequestPaginationDto request, IQueryable<T> data)
    {
        TotalCount = data.Count();
        PageSize = request.PageSize > 0 ? request.PageSize : 1;
        TotalPages = (int)Math.Ceiling((double)TotalCount / PageSize);

        Data = data
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize);

        PageNumber = request.PageNumber;
        PageSize = request.PageSize;
    }
    
    public IQueryable<T> Data { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
