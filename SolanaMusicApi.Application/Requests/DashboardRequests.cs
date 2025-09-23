using MediatR;
using SolanaMusicApi.Domain.DTO.Artist;
using SolanaMusicApi.Domain.DTO.Artist.ArtistApplication;
using SolanaMusicApi.Domain.DTO.Dashboard;
using SolanaMusicApi.Domain.DTO.Dashboard.Overview;
using SolanaMusicApi.Domain.DTO.Pagination;
using SolanaMusicApi.Domain.DTO.Sorting;
using SolanaMusicApi.Domain.DTO.User;
using SolanaMusicApi.Domain.Enums;

namespace SolanaMusicApi.Application.Requests;

public record GetDashboardOverviewRequest : IRequest<DashboardOverviewResponseDto>;
public record GetUsersRequest(DashboardFilter Filter, RequestSortingDto Sorting) 
    : IRequest<PaginationResponseDto<UserResponseDto>>;
public record GetArtistsDashboardRequest(DashboardFilter Filter, RequestSortingDto Sorting) 
    : IRequest<PaginationResponseDto<ArtistResponseDto>>;
public record GetPendingApplications : IRequest<int>;
public record GetArtistApplicationsRequest(DashboardFilter Filter, RequestSortingDto Sorting, ArtistApplicationStatus? Status) 
    : IRequest<PaginationResponseDto<ArtistApplicationResponseDto>>;