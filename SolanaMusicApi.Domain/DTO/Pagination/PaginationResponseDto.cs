namespace SolanaMusicApi.Domain.DTO.Pagination;

public class PaginationResponseDto<T>
{
    public List<T> Data { get; set; } = [];
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}