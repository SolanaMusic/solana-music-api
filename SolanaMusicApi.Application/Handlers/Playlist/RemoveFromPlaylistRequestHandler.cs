using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.PlaylistServices.PlaylistService;

namespace SolanaMusicApi.Application.Handlers.Playlist;

public class RemoveFromPlaylistRequestHandler(IPlaylistService playlistService) : IRequestHandler<RemoveFromPlaylistRequest>
{
    public async Task Handle(RemoveFromPlaylistRequest request, CancellationToken cancellationToken)
    {
        await playlistService.RemoveFromPlaylistAsync(request.AddToPlaylistDto);
    }
}
