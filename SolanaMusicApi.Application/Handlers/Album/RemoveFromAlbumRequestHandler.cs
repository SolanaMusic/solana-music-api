using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.AlbumService;

namespace SolanaMusicApi.Application.Handlers.Album;

public class RemoveFromAlbumRequestHandler(IAlbumService albumService) : IRequestHandler<RemoveFromAlbumRequest>
{
    public async Task Handle(RemoveFromAlbumRequest request, CancellationToken cancellationToken)
    {
        await albumService.RemoveFromAlbumAsync(request.TrackId);
    }
}
