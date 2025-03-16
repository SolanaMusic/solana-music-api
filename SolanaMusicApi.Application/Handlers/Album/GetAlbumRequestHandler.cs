using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.AlbumService;
using SolanaMusicApi.Domain.DTO.Album;

namespace SolanaMusicApi.Application.Handlers.Album;

public class GetAlbumRequestHandler(IAlbumService albumService) : IRequestHandler<GetAlbumRequest, AlbumResponseDto>
{
    public Task<AlbumResponseDto> Handle(GetAlbumRequest request, CancellationToken cancellationToken)
    {
        return albumService.GetAlbumAsync(request.Id);
    }
}
