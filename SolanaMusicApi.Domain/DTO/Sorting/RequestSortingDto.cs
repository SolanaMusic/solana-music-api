using Microsoft.AspNetCore.Mvc;
using SolanaMusicApi.Domain.Enums;

namespace SolanaMusicApi.Domain.DTO.Sorting;

public class RequestSortingDto
{
    [FromQuery(Name = "sortColumn")]
    public string SortColumn { get; set; } = string.Empty;
    
    [FromQuery(Name = "sortDirection")]
    public SortDirection SortDirection { get; set; }
}