using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests.Genre;
using SolanaMusicApi.Application.Services.GenreService;
using SolanaMusicApi.Domain.DTO.Genre;
using GenreEntity = SolanaMusicApi.Domain.Entities.Music.Genre;

namespace SolanaMusicApi.Application.Handlers.Genre;

public class CreateGenreRequestHandler(IGenreService genreService, IMapper mapper) : IRequestHandler<CreateGenreRequest, GenreResponseDto>
{
    public async Task<GenreResponseDto> Handle(CreateGenreRequest request, CancellationToken cancellationToken)
    {
        var genre = mapper.Map<GenreEntity>(request.GenreRequestDto);
        var response = await genreService.AddAsync(genre);
        return mapper.Map<GenreResponseDto>(response);
    }
}
