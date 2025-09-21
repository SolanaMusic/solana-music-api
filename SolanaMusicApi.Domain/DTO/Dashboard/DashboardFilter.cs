using Microsoft.AspNetCore.Mvc;
using SolanaMusicApi.Domain.DTO.Pagination;
using SolanaMusicApi.Domain.Enums;

namespace SolanaMusicApi.Domain.DTO.Dashboard;

public class DashboardFilter : RequestPaginationDto
{
    [FromQuery(Name = "query")]
    public string Query { get; set; } = string.Empty;
    
    [FromQuery(Name = "timeFilter")]
    public TimeFilter TimeFilter { get; set; }
}