using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests.Artist;
using SolanaMusicApi.Application.Services.ArtistServices.ArtistService;
using SolanaMusicApi.Domain.DTO.Artist;
using ArtistEntity = SolanaMusicApi.Domain.Entities.Performer.Artist;

namespace SolanaMusicApi.Application.Handlers.Artist;

public class UpdateArtistRequestHandler(IArtistService artistService, IMapper mapper) : IRequestHandler<UpdateArtistRequest, ArtistResponseDto>
{
    public async Task<ArtistResponseDto> Handle(UpdateArtistRequest request, CancellationToken cancellationToken)
    {
        var artist = mapper.Map<ArtistEntity>(request.ArtistRequestDto);
        var response = await artistService.UpdateArtistAsync(request.Id, request.ArtistRequestDto);
        return mapper.Map<ArtistResponseDto>(response);
    }
}
