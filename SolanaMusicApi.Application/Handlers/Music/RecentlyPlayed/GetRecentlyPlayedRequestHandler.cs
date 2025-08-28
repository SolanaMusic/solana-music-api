using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.TrackServices.RecentlyPlayedService;
using SolanaMusicApi.Application.Services.TrackServices.TracksService;
using SolanaMusicApi.Domain.DTO.Track;

namespace SolanaMusicApi.Application.Handlers.Music.RecentlyPlayed;

public class GetRecentlyPlayedRequestHandler(IRecentlyPlayedService recentlyPlayedService, ITracksService tracksService, IMapper mapper) 
    :  IRequestHandler<GetRecentlyPlayedRequest, List<TrackResponseDto>>
{
    public async Task<List<TrackResponseDto>> Handle(GetRecentlyPlayedRequest request, CancellationToken cancellationToken)
    {
        var ids = await recentlyPlayedService
            .GetAll()
            .Where(x => x.UserId == request.UserId)
            .OrderByDescending(x => x.UpdatedDate)
            .Take(5)
            .Select(x => x.TrackId)
            .ToListAsync(cancellationToken);
        
        var response = await tracksService.GetTracksQueryAsync();
        
        var tracks = await response
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(cancellationToken);
        
        foreach (var track in tracks)
            tracksService.MapProperties(track);
        
        return mapper.Map<List<TrackResponseDto>>(tracks);
    }
}