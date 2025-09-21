using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.ArtistServices.ArtistApplicationService;

namespace SolanaMusicApi.Application.Handlers.Artist.ArtistApplication;

public class DeleteArtistApplicationRequestHandler(IArtistApplicationService artistApplicationService) 
    :  IRequestHandler<DeleteArtistApplicationRequest>
{
    public async Task Handle(DeleteArtistApplicationRequest request, CancellationToken cancellationToken) => 
        await artistApplicationService.DeleteAsync(request.Id);
}