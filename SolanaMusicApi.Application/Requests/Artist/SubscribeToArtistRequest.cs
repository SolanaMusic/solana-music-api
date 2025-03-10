using MediatR;

namespace SolanaMusicApi.Application.Requests.Artist;

public record SubscribeToArtistRequest(long Id, long UserId) : IRequest;
