using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.GenreService;
using SolanaMusicApi.Domain.DTO.Genre;

namespace SolanaMusicApi.Application.Handlers.Genre;

public class GetGenreByNameRequestHandler(IGenreService genreService, IMapper mapper) 
    : IRequestHandler<GetGenresByNameRequest, List<GenreResponseDto>>
{
    public async Task<List<GenreResponseDto>> Handle(GetGenresByNameRequest request, CancellationToken cancellationToken)
    {
        var query = genreService.GetAll();

        if (!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(x => EF.Functions.Like(x.Name, $"%{request.Name}%"));

        var response = await query.ToListAsync(cancellationToken);
        return mapper.Map<List<GenreResponseDto>>(response);
    }
}