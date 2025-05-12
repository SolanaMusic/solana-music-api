using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.TrackServices.TracksService;
using SolanaMusicApi.Domain.DTO.Track;

namespace SolanaMusicApi.Application.Handlers.Music;

public class GetTracksByArtistRequestHandler(ITracksService tracksService, IMapper mapper) : IRequestHandler<GetTracksByArtistRequest, List<TrackResponseDto>>
{
    public async Task<List<TrackResponseDto>> Handle(GetTracksByArtistRequest request, CancellationToken cancellationToken)
    {
        var query = await tracksService.GetTracksQueryAsync();
        query = query.Where(x => x.ArtistTracks.Any(at => at.ArtistId == request.ArtistId));
        
        if (!string.IsNullOrEmpty(request.Name))
            query = query.Where(track => EF.Functions.Like(track.Title, $"%{request.Name}%"));
        
        var response = await query.ToListAsync(cancellationToken);
        
        foreach (var track in response)
            tracksService.MapProperties(track);
        
        return mapper.Map<List<TrackResponseDto>>(response);
    }
}