using MediatR;
using SolanaMusicApi.Application.Requests.Genre;
using SolanaMusicApi.Application.Services.GenreService;

namespace SolanaMusicApi.Application.Handlers.Genre;

public class DeleteGenreRequestHandler(IGenreService genreService) : IRequestHandler<DeleteGenreRequest, bool>
{
    public Task<bool> Handle(DeleteGenreRequest request, CancellationToken cancellationToken)
    {
        return genreService.DeleteAsync(request.Id);
    }
}
