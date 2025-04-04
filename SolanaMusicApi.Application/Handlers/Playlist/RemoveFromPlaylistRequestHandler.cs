using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.PlaylistServices.PlaylistService;
using SolanaMusicApi.Domain.DTO.Playlist;

namespace SolanaMusicApi.Application.Handlers.Playlist;

public class RemoveFromPlaylistRequestHandler(IPlaylistService playlistService) : IRequestHandler<RemoveFromPlaylistRequest>
{
    public async Task Handle(RemoveFromPlaylistRequest request, CancellationToken cancellationToken)
    {
        var requestDto = new AddToPlaylistDto { PlaylistId = request.PlaylistId, TrackId = request.TrackId };
        await playlistService.RemoveFromPlaylistAsync(requestDto);
    }
}
