using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests.Music;
using SolanaMusicApi.Application.Services.TracksService;
using SolanaMusicApi.Domain.DTO.Track;

namespace SolanaMusicApi.Application.Handlers.Music;

public class GetTracksRequestHandler(ITracksService tracksService) : IRequestHandler<GetTracksRequest, List<TrackResponseDto>>
{
    public async Task<List<TrackResponseDto>> Handle(GetTracksRequest request, CancellationToken cancellationToken)
    {
        return await tracksService.GetTracksAsync();
    }
}
