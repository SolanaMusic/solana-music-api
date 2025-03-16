using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.TrackServices.TracksService;
using SolanaMusicApi.Domain.DTO.Track;

namespace SolanaMusicApi.Application.Handlers.Music;

public class CreateTrackRequestHandler(ITracksService tracksService) : IRequestHandler<CreateTrackRequest, TrackResponseDto>
{
    public async Task<TrackResponseDto> Handle(CreateTrackRequest request, CancellationToken cancellationToken)
    {
        return await tracksService.CreateTrackAsync(request.TrackRequestDto);
    }
}
