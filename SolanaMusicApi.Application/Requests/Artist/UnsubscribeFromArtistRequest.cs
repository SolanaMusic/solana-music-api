using MediatR;

namespace SolanaMusicApi.Application.Requests.Artist;

public record UnsubscribeFromArtistRequest(long Id, long UserId) : IRequest;
