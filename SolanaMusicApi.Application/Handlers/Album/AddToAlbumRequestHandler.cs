using MediatR;
using SolanaMusicApi.Application.Requests.Album;
using SolanaMusicApi.Application.Services.AlbumService;

namespace SolanaMusicApi.Application.Handlers.Album;

public class AddToAlbumRequestHandler(IAlbumService albumService) : IRequestHandler<AddToAlbumRequest>
{
    public async Task Handle(AddToAlbumRequest request, CancellationToken cancellationToken)
    {
        await albumService.AddToAlbumAsync(request.AddToAlbumDto);
    }
}
