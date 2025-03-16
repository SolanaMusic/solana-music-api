using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.TrackServices.TracksService;

namespace SolanaMusicApi.Application.Handlers.Music;

public class PlayTrackRequestHandler(ITracksService tracksService) : IRequestHandler<StreamTrackRequest, FileStream>
{
    public async Task<FileStream> Handle(StreamTrackRequest request, CancellationToken cancellationToken)
    {
        return await tracksService.GetTrackFileStreamAsync(request.Id);
    }
}
