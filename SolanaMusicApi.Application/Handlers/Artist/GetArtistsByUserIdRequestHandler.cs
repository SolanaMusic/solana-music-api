using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.ArtistServices.ArtistService;
using SolanaMusicApi.Domain.DTO.Artist;

namespace SolanaMusicApi.Application.Handlers.Artist;

public class GetArtistsByUserIdRequestHandler(IArtistService artistService, IMapper mapper) 
    : IRequestHandler<GetArtistsByUserIdRequest, ArtistResponseDto>
{
    public async Task<ArtistResponseDto> Handle(GetArtistsByUserIdRequest request, CancellationToken cancellationToken)
    {
        var response = await artistService
            .GetArtists()
            .FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);
        
        return mapper.Map<ArtistResponseDto>(response);
    }
}