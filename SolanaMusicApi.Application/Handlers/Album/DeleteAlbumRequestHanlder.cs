using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.AlbumService;
using SolanaMusicApi.Domain.DTO.Album;

namespace SolanaMusicApi.Application.Handlers.Album;

public class DeleteAlbumRequestHanlder(IAlbumService albumService) : IRequestHandler<DeleteAlbumRequest, AlbumResponseDto>
{
    public async Task<AlbumResponseDto> Handle(DeleteAlbumRequest request, CancellationToken cancellationToken)
    {
        return await albumService.DeleteAlbumAsync(request.Id);
    }
}
