using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.TrackServices.TracksService;
using SolanaMusicApi.Domain.DTO.Track;

namespace SolanaMusicApi.Application.Handlers.Music;

public class UpdateTrackRequestHandler(ITracksService tracksService) : IRequestHandler<UpdateTrackRequest, TrackResponseDto>
{
    public async Task<TrackResponseDto> Handle(UpdateTrackRequest request, CancellationToken cancellationToken)
    {
        return await tracksService.UpdateTrackAsync(request.Id, request.TrackRequestDto);
    }
}
