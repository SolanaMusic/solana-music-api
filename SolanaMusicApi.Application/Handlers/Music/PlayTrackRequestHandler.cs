using MediatR;
using SolanaMusicApi.Application.Requests.Music;
using SolanaMusicApi.Application.Services.TracksService;

namespace SolanaMusicApi.Application.Handlers.Music;

public class PlayTrackRequestHandler(ITracksService tracksService) : IRequestHandler<PlayTrackRequest, FileStream>
{
    public async Task<FileStream> Handle(PlayTrackRequest request, CancellationToken cancellationToken)
    {
        var response = await tracksService.GetByIdAsync(request.Id); 
        return new FileStream(response.FileUrl, FileMode.Open, FileAccess.Read, FileShare.Read);
    }
}
