using MediatR;
using SolanaMusicApi.Application.Requests.Album;
using SolanaMusicApi.Application.Services.AlbumService;
using SolanaMusicApi.Domain.DTO.Album;

namespace SolanaMusicApi.Application.Handlers.Album;

public class CreateAlbumRequestHandler(IAlbumService albumService) : IRequestHandler<CreateAlbumRequest, AlbumResponseDto>
{
    public async Task<AlbumResponseDto> Handle(CreateAlbumRequest request, CancellationToken cancellationToken)
    {
        return await albumService.CreateAlbumAsync(request.AlbumRequestDto);
    }
}
