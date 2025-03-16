using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.AlbumService;
using SolanaMusicApi.Domain.DTO.Album;

namespace SolanaMusicApi.Application.Handlers.Album;

public class GetAlbumsRequestHandler(IAlbumService albumService, IMapper mapper) : IRequestHandler<GetAlbumsRequest, List<AlbumResponseDto>>
{
    public Task<List<AlbumResponseDto>> Handle(GetAlbumsRequest request, CancellationToken cancellationToken)
    {
        var response = albumService.GetAlbums();
        return Task.FromResult(mapper.Map<List<AlbumResponseDto>>(response));
    }
}
