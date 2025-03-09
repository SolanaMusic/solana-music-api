using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Requests.Playlist;
using SolanaMusicApi.Application.Services.PlaylistServices.PlaylistService;
using SolanaMusicApi.Domain.DTO.Playlist;

namespace SolanaMusicApi.Application.Handlers.Playlist;

public class GetUserPlaylistsRequestHandler(IPlaylistService playlistService, IMapper mapper) : 
    IRequestHandler<GetUserPlaylistsRequest, List<PlaylistResponseDto>>
{
    public async Task<List<PlaylistResponseDto>> Handle(GetUserPlaylistsRequest request, CancellationToken cancellationToken)
    {
        var response = await playlistService.GetPlaylists(request.UserId).ToListAsync();
        return mapper.Map<List<PlaylistResponseDto>>(response);
    }
}
