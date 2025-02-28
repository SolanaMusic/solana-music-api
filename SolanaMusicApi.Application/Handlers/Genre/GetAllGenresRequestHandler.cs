using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests.Genre;
using SolanaMusicApi.Application.Services.GenreService;
using SolanaMusicApi.Domain.DTO.Genre;

namespace SolanaMusicApi.Application.Handlers.Genre;

public class GetAllGenresRequestHandler(IGenreService genreService, IMapper mapper) : IRequestHandler<GetAllGenresRequest, List<GenreResponseDto>>
{
    public Task<List<GenreResponseDto>> Handle(GetAllGenresRequest request, CancellationToken cancellationToken)
    {
        var genres = genreService.GetAll();
        var response = mapper.Map<List<GenreResponseDto>>(genres);
        return Task.FromResult(response);
    }
}
