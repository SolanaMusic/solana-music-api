using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.ArtistServices.ArtistService;
using SolanaMusicApi.Domain.DTO.Artist;

namespace SolanaMusicApi.Application.Handlers.Artist;

public class GetArtistsByNameRequestHandler(IArtistService artistService, IMapper mapper) 
    : IRequestHandler<GetArtistsByNameRequest, List<ArtistResponseDto>>
{
    public async Task<List<ArtistResponseDto>> Handle(GetArtistsByNameRequest request, CancellationToken cancellationToken)
    {
        var query = artistService.GetAll();

        if (!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(x => EF.Functions.Like(x.Name, $"%{request.Name}%"));

        var response = await query.ToListAsync(cancellationToken);
        return mapper.Map<List<ArtistResponseDto>>(response);
    }
}