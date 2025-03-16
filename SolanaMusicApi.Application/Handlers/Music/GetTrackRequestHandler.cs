using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.TrackServices.TracksService;
using SolanaMusicApi.Domain.DTO.Track;

namespace SolanaMusicApi.Application.Handlers.Music;

public class GetTrackRequestHandler(ITracksService tracksService) : IRequestHandler<GetTrackRequest, TrackResponseDto>
{
    public async Task<TrackResponseDto> Handle(GetTrackRequest request, CancellationToken cancellationToken)
    {
        return await tracksService.GetTrackAsync(request.Id);
    }
}
