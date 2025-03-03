using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests.Genre;
using SolanaMusicApi.Application.Services.GenreService;
using SolanaMusicApi.Domain.DTO.Genre;

namespace SolanaMusicApi.Application.Handlers.Genre;

public class DeleteGenreRequestHandler(IGenreService genreService, IMapper mapper) : IRequestHandler<DeleteGenreRequest, GenreResponseDto>
{
    public async Task<GenreResponseDto> Handle(DeleteGenreRequest request, CancellationToken cancellationToken)
    {
        var response = await genreService.DeleteAsync(request.Id);
        return mapper.Map<GenreResponseDto>(response);
    }
}
