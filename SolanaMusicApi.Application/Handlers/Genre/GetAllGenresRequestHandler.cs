using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.GenreService;
using SolanaMusicApi.Domain.DTO.Genre;

namespace SolanaMusicApi.Application.Handlers.Genre;

public class GetAllGenresRequestHandler(IGenreService genreService, IMapper mapper) : IRequestHandler<GetGenresRequest, List<GenreResponseDto>>
{
    public Task<List<GenreResponseDto>> Handle(GetGenresRequest request, CancellationToken cancellationToken)
    {
        var genres = genreService.GetAll();
        var response = mapper.Map<List<GenreResponseDto>>(genres);
        return Task.FromResult(response);
    }
}
