using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.PlaylistServices.PlaylistService;
using SolanaMusicApi.Domain.DTO.Playlist;

namespace SolanaMusicApi.Application.Handlers.Playlist;

public class UpdatePlaylistRequestHandler(IPlaylistService playlistService, IMapper mapper) : 
    IRequestHandler<UpdatePlaylistRequest, PlaylistResponseDto>
{
    public async Task<PlaylistResponseDto> Handle(UpdatePlaylistRequest request, CancellationToken cancellationToken)
    {
        var response = await playlistService.UpdatePlaylistAsync(request.Id, request.PlaylistRequestDto);
        return mapper.Map<PlaylistResponseDto>(response);
    }
}
