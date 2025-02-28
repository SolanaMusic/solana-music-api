using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests.Genre;
using SolanaMusicApi.Application.Services.GenreService;
using SolanaMusicApi.Domain.DTO.Genre;
using GenreEntity = SolanaMusicApi.Domain.Entities.Music.Genre;

namespace SolanaMusicApi.Application.Handlers.Genre;

public class UpdateGenreRequestHandler(IGenreService genreService, IMapper mapper) : IRequestHandler<UpdateGenreRequest, GenreResponseDto>
{
    public async Task<GenreResponseDto> Handle(UpdateGenreRequest request, CancellationToken cancellationToken)
    {
        var genre = mapper.Map<GenreEntity>(request.GenreRequestDto);
        var response = await genreService.UpdateAsync(request.Id, genre);
        return mapper.Map<GenreResponseDto>(response);
    }
}
