using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.ArtistServices.ArtistService;
using SolanaMusicApi.Domain.DTO.Artist;
using ArtistEntity = SolanaMusicApi.Domain.Entities.Performer.Artist;

namespace SolanaMusicApi.Application.Handlers.Artist;

public class CreateArtistRequestHandler(IArtistService artistService, IMapper mapper) : IRequestHandler<CreateArtistRequest, ArtistResponseDto>
{
    public async Task<ArtistResponseDto> Handle(CreateArtistRequest request, CancellationToken cancellationToken)
    {
        var artist = mapper.Map<ArtistEntity>(request.ArtistRequestDto);
        var response = await artistService.CreateArtistAsync(artist, request.ArtistRequestDto.ImageFile);
        return mapper.Map<ArtistResponseDto>(response);
    }
}
