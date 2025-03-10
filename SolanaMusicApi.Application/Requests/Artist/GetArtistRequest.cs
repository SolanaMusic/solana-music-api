using MediatR;
using SolanaMusicApi.Domain.DTO.Artist;

namespace SolanaMusicApi.Application.Requests.Artist;

public record GetArtistRequest(long Id, long UserId) : IRequest<ArtistResponseDto>;
