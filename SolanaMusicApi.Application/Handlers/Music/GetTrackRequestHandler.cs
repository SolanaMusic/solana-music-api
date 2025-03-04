using MediatR;
using SolanaMusicApi.Application.Requests.Music;
using SolanaMusicApi.Application.Services.TracksService;
using SolanaMusicApi.Domain.DTO.Track;

namespace SolanaMusicApi.Application.Handlers.Music;

public class GetTrackRequestHandler(ITracksService tracksService) : IRequestHandler<GetTrackRequest, TrackResponseDto>
{
    public async Task<TrackResponseDto> Handle(GetTrackRequest request, CancellationToken cancellationToken)
    {
        return await tracksService.GetTrackAsync(request.Id);
    }
}
