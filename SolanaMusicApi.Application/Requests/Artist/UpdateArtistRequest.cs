using MediatR;
using SolanaMusicApi.Domain.DTO.Artist;

namespace SolanaMusicApi.Application.Requests.Artist;

public record UpdateArtistRequest(long Id, ArtistRequestDto ArtistRequestDto) : IRequest<ArtistResponseDto>;
