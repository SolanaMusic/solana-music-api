using MediatR;
using SolanaMusicApi.Application.Requests.Music;
using SolanaMusicApi.Application.Services.TrackServices.TracksService;

namespace SolanaMusicApi.Application.Handlers.Music;

public class PlayTrackRequestHandler(ITracksService tracksService) : IRequestHandler<PlayTrackRequest, FileStream>
{
    public async Task<FileStream> Handle(PlayTrackRequest request, CancellationToken cancellationToken)
    {
        return await tracksService.GetTrackFileStreamAsync(request.Id);
    }
}
