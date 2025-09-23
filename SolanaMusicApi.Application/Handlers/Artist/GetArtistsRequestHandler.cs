using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.ArtistServices.ArtistService;
using SolanaMusicApi.Domain.DTO.Artist;

namespace SolanaMusicApi.Application.Handlers.Artist;

public class GetArtistsRequestHandler(IArtistService artistService, IMapper mapper) : IRequestHandler<GetArtistsRequest, List<ArtistResponseDto>>
{
    public async Task<List<ArtistResponseDto>> Handle(GetArtistsRequest request, CancellationToken cancellationToken)
    {
        var artists = await artistService.GetArtists().ToListAsync(cancellationToken);
        var response = mapper.Map<List<ArtistResponseDto>>(artists);

        foreach (var artistDto in response)
        {
            var artist = artists.FirstOrDefault(a => a.Id == artistDto.Id);

            if (artist != null && artistService.CheckArtistSubscription(artist, request.UserId))
                artistDto.IsUserSubscribed = true;
        }

        return response;
    }
}
