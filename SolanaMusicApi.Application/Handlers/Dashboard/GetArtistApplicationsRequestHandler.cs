using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.ArtistServices.ArtistApplicationService;
using SolanaMusicApi.Domain.DTO.Artist.ArtistApplication;
using SolanaMusicApi.Domain.DTO.Dashboard;
using SolanaMusicApi.Domain.DTO.Pagination;
using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Domain.Enums;
using SolanaMusicApi.Application.Extensions;

namespace SolanaMusicApi.Application.Handlers.Dashboard;

public class GetArtistApplicationsRequestHandler(IArtistApplicationService artistApplicationService, IMapper mapper)
    : IRequestHandler<GetArtistApplicationsRequest, PaginationResponseDto<ArtistApplicationResponseDto>>
{
    public Task<PaginationResponseDto<ArtistApplicationResponseDto>> Handle(GetArtistApplicationsRequest request, CancellationToken cancellationToken)
    {
        var applications = artistApplicationService.GetUsersApplications();
        applications = applications.ApplySorting(request.Sorting);
        
        if (request.Status.HasValue && request.Status.Value != ArtistApplicationStatus.All)
            applications = applications.Where(x => x.Status == request.Status.Value);

        if (!string.IsNullOrEmpty(request.Filter.Query))
        {
            applications = applications.Where(x => x.User.Email != null && x.User.UserName 
                != null && (x.User.Email.Contains(request.Filter.Query) || x.User.UserName.Contains(request.Filter.Query)));
        }
        
        var paginated = new DashboardResponsePaginationDto<ArtistApplication>(request.Filter, applications, x => x.CreatedDate);
        var response = mapper.Map<PaginationResponseDto<ArtistApplicationResponseDto>>(paginated);
        return Task.FromResult(response);
    }
}