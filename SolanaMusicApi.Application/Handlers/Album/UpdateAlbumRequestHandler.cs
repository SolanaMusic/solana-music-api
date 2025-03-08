using MediatR;
using SolanaMusicApi.Application.Requests.Album;
using SolanaMusicApi.Application.Services.AlbumService;
using SolanaMusicApi.Domain.DTO.Album;

namespace SolanaMusicApi.Application.Handlers.Album;

public class UpdateAlbumRequestHandler(IAlbumService albumService) : IRequestHandler<UpdateAlbumRequest, AlbumResponseDto>
{
    public async Task<AlbumResponseDto> Handle(UpdateAlbumRequest request, CancellationToken cancellationToken)
    {
        return await albumService.UpdateAlbumAsync(request.Id, request.AlbumRequestDto);
    }
}
