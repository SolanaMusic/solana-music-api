using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Extensions;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.ArtistServices.ArtistService;
using SolanaMusicApi.Domain.DTO.Artist;
using SolanaMusicApi.Domain.DTO.Dashboard;
using SolanaMusicApi.Domain.DTO.Pagination;

namespace SolanaMusicApi.Application.Handlers.Dashboard;

public class GetArtistsDashboardRequestHandler(IArtistService artistService, IMapper mapper) 
    : IRequestHandler<GetArtistsDashboardRequest, PaginationResponseDto<ArtistResponseDto>>
{
    public Task<PaginationResponseDto<ArtistResponseDto>> Handle(GetArtistsDashboardRequest request, CancellationToken cancellationToken)
    {
        var artists =  artistService.GetArtists();
        artists = artists.ApplySorting(request.Sorting);
        
        if (!string.IsNullOrEmpty(request.Filter.Query))
            artists = artists.Where(x => EF.Functions.Like(x.Name, $"%{request.Filter.Query}%"));
        
        var paginated = new DashboardResponsePaginationDto<Domain.Entities.Performer.Artist>(request.Filter, artists, x => x.CreatedDate);
        var response = mapper.Map<PaginationResponseDto<ArtistResponseDto>>(paginated);
        return Task.FromResult(response);
    }
}