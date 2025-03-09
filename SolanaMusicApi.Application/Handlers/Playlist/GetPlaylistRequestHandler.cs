using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests.Playlist;
using SolanaMusicApi.Application.Services.PlaylistServices.PlaylistService;
using SolanaMusicApi.Domain.DTO.Playlist;

namespace SolanaMusicApi.Application.Handlers.Playlist;

public class GetPlaylistRequestHandler(IPlaylistService playlistService, IMapper mapper) : IRequestHandler<GetPlaylistRequest, PlaylistResponseDto>
{
    public async Task<PlaylistResponseDto> Handle(GetPlaylistRequest request, CancellationToken cancellationToken)
    {
        var response = await playlistService.GetPlaylistAsync(request.Id);
        return mapper.Map<PlaylistResponseDto>(response);
    }
}
