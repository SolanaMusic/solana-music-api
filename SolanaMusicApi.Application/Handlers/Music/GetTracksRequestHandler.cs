using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.TrackServices.TracksService;
using SolanaMusicApi.Domain.DTO.Track;

namespace SolanaMusicApi.Application.Handlers.Music;

public class GetTracksRequestHandler(ITracksService tracksService, IMapper mapper) : IRequestHandler<GetTracksRequest, List<TrackResponseDto>>
{
    public async Task<List<TrackResponseDto>> Handle(GetTracksRequest request, CancellationToken cancellationToken)
    {
        var response = await tracksService.GetTracksQueryAsync();
        var tracks = await response.ToListAsync(cancellationToken);
        
        foreach (var track in tracks)
            tracksService.MapProperties(track);
        
        return mapper.Map<List<TrackResponseDto>>(tracks);
    }
}
