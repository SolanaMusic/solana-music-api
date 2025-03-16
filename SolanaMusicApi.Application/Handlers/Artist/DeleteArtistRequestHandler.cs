using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.ArtistServices.ArtistService;
using SolanaMusicApi.Domain.DTO.Artist;

namespace SolanaMusicApi.Application.Handlers.Artist;

public class DeleteArtistRequestHandler(IArtistService artistService, IMapper mapper) : IRequestHandler<DeleteArtistRequest, ArtistResponseDto>
{
    public async Task<ArtistResponseDto> Handle(DeleteArtistRequest request, CancellationToken cancellationToken)
    {
        var response = await artistService.DeleteArtistAsync(request.Id);
        return mapper.Map<ArtistResponseDto>(response);
    }
}
