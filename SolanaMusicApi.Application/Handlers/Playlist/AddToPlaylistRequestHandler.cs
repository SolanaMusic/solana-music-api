using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.PlaylistServices.PlaylistService;

namespace SolanaMusicApi.Application.Handlers.Playlist;

public class AddToPlaylistRequestHandler(IPlaylistService playlistService) : IRequestHandler<AddToPlaylistRequest>
{
    public async Task Handle(AddToPlaylistRequest request, CancellationToken cancellationToken)
    {
        await playlistService.AddToPlaylistAsync(request.AddToPlaylistDto);
    }
}
