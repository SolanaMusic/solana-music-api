using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.PlaylistServices.PlaylistService;
using SolanaMusicApi.Domain.DTO.Playlist;

namespace SolanaMusicApi.Application.Handlers.Playlist;

public class DeletePlaylistRequestHandler(IPlaylistService playlistService, IMapper mapper) :
    IRequestHandler<DeletePlaylistRequest, PlaylistResponseDto>
{
    public async Task<PlaylistResponseDto> Handle(DeletePlaylistRequest request, CancellationToken cancellationToken)
    {
        var response = await playlistService.DeletePlaylistAsync(request.Id);
        return mapper.Map<PlaylistResponseDto>(response);
    }
}
