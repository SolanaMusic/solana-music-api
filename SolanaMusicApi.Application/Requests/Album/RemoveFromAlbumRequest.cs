using MediatR;

namespace SolanaMusicApi.Application.Requests.Album;

public record RemoveFromAlbumRequest(long TrackId) : IRequest;
