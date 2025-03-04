using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests.Music;
using SolanaMusicApi.Application.Services.TracksService;
using SolanaMusicApi.Domain.DTO.Track;

namespace SolanaMusicApi.Application.Handlers.Music;

public class DeleteTrackRequestHandler(ITracksService tracksService, IMapper mapper) : IRequestHandler<DeleteTrackRequest, TrackResponseDto>
{
    public async Task<TrackResponseDto> Handle(DeleteTrackRequest request, CancellationToken cancellationToken)
    {
        var response = await tracksService.DeleteTrackAsync(request.Id);
        return mapper.Map<TrackResponseDto>(response);
    }
}
