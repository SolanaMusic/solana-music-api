using MediatR;
using SolanaMusicApi.Domain.DTO.Artist;

namespace SolanaMusicApi.Application.Requests.Artist;

public record CreateArtistRequest(ArtistRequestDto ArtistRequestDto) : IRequest<ArtistResponseDto>;
