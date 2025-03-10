using MediatR;
using SolanaMusicApi.Domain.DTO.Artist;

namespace SolanaMusicApi.Application.Requests.Artist;

public record GetArtistsRequest(long UserId) : IRequest<List<ArtistResponseDto>>;
