using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SolanaMusicApi.Domain.DTO.Pagination;

public class RequestPaginationDto
{
    [Required]
    [FromQuery(Name = "pageNumber")]
    public int PageNumber { get; set; } = 1;

    [Required]
    [FromQuery(Name = "pageSize")]
    public int PageSize { get; set; } = 20;
}
