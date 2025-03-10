using MediatR;
using SolanaMusicApi.Application.Requests.Artist;
using SolanaMusicApi.Application.Services.ArtistServices.ArtistService;

namespace SolanaMusicApi.Application.Handlers.Artist;

public class UnsubscribeFromArtistRequestHandler(IArtistService artistService) : IRequestHandler<UnsubscribeFromArtistRequest>
{
    public async Task Handle(UnsubscribeFromArtistRequest request, CancellationToken cancellationToken)
    {
        await artistService.UnsubscribeFromArtist(request.Id, request.UserId);
    }
}
