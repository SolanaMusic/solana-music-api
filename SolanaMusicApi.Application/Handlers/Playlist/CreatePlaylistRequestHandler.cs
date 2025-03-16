using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.PlaylistServices.PlaylistService;
using SolanaMusicApi.Domain.DTO.Playlist;
using PlaylistEntity = SolanaMusicApi.Domain.Entities.Playlist.Playlist;

namespace SolanaMusicApi.Application.Handlers.Playlist;

public class CreatePlaylistRequestHandler(IPlaylistService playlistService, IMapper mapper) : 
    IRequestHandler<CreatePlaylistRequest, PlaylistResponseDto>
{
    public async Task<PlaylistResponseDto> Handle(CreatePlaylistRequest request, CancellationToken cancellationToken)
    {
        var playlist = mapper.Map<PlaylistEntity>(request.PlaylistRequestDto);
        var response = await playlistService.CreatePlaylistAsync(playlist, request.PlaylistRequestDto);
        return mapper.Map<PlaylistResponseDto>(response);
    }
}
