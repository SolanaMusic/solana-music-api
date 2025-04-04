using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.ArtistServices.ArtistService;

namespace SolanaMusicApi.Application.Handlers.Artist;

public class SubscribeToArtistRequestHandler(IArtistService artistService) : IRequestHandler<SubscribeToArtistRequest>
{
    public async Task Handle(SubscribeToArtistRequest request, CancellationToken cancellationToken)
    {
        await artistService.SubscribeToArtist(request.SubscribeToArtistDto.Id, request.SubscribeToArtistDto.UserId);
    }
}
