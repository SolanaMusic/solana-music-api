using MediatR;

namespace SolanaMusicApi.Application.Requests.Album;

public record AddToAlbumRequest(long? AlbumId, long TrackId) : IRequest;
