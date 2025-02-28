using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests.Genre;
using SolanaMusicApi.Application.Services.GenreService;
using SolanaMusicApi.Domain.DTO.Genre;

namespace SolanaMusicApi.Application.Handlers.Genre;

public class GetGenreRequestHandler(IGenreService genreService, IMapper mapper) : IRequestHandler<GetGenreRequest, GenreResponseDto>
{
    public async Task<GenreResponseDto> Handle(GetGenreRequest request, CancellationToken cancellationToken)
    {
        var response = await genreService.GetByIdAsync(request.Id);
        return mapper.Map<GenreResponseDto>(response);
    }
}
