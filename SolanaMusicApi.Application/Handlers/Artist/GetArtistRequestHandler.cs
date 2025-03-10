using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests.Artist;
using SolanaMusicApi.Application.Services.ArtistServices.ArtistService;
using SolanaMusicApi.Domain.DTO.Artist;

namespace SolanaMusicApi.Application.Handlers.Artist;

public class GetArtistRequestHandler(IArtistService artistService, IMapper mapper) : IRequestHandler<GetArtistRequest, ArtistResponseDto>
{
    public async Task<ArtistResponseDto> Handle(GetArtistRequest request, CancellationToken cancellationToken)
    {
        var artist = await artistService.GetArtistAsync(request.Id);
        var response = mapper.Map<ArtistResponseDto>(artist);

        if (artist != null && artistService.CheckArtistSubscription(artist, request.UserId))
            response.IsUserSubscribed = true;

        return response;
    }
}
