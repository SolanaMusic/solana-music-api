using MediatR;
using SolanaMusicApi.Domain.DTO.Artist;

namespace SolanaMusicApi.Application.Requests.Artist;

public record DeleteArtistRequest(long Id) : IRequest<ArtistResponseDto>;
