using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Extensions;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.TrackServices.TracksService;
using SolanaMusicApi.Domain.DTO.Dashboard;
using SolanaMusicApi.Domain.DTO.Pagination;
using SolanaMusicApi.Domain.DTO.Track;
using SolanaMusicApi.Domain.Entities.Music;

namespace SolanaMusicApi.Application.Handlers.Dashboard;

public class GetDashboardTracksRequestHandler(ITracksService tracksService, IMapper mapper) 
    : IRequestHandler<GetDashboardTracksRequest, PaginationResponseDto<TrackResponseDto>>
{
    public async Task<PaginationResponseDto<TrackResponseDto>> Handle(GetDashboardTracksRequest request, CancellationToken cancellationToken)
    {
        var tracks = await tracksService.GetTracksQueryAsync();
        tracks = tracks.ApplySorting(request.Sorting);
        
        if (!string.IsNullOrEmpty(request.Filter.Query))
        {
            tracks = tracks.Where(x => 
                    EF.Functions.Like(x.Title, $"%{request.Filter.Query}%")
                    || x.ArtistTracks.Any(at => EF.Functions.Like(at.Artist.Name, $"%{request.Filter.Query}%"))
                    || EF.Functions.Like(x.Album.Title, $"%{request.Filter.Query}%")
            );
        }
        
        var paginated = new DashboardResponsePaginationDto<Track>(request.Filter, tracks, x => x.ReleaseDate);
        var tracksList = await paginated.Data.ToListAsync(cancellationToken);
        
        foreach (var track in tracksList)
            tracksService.MapProperties(track);
        
        paginated.Data = tracksList.AsQueryable();
        var response = mapper.Map<PaginationResponseDto<TrackResponseDto>>(paginated);
        return response;
    }
}