using MediatR;
using SolanaMusicApi.Domain.DTO.Playlist;

namespace SolanaMusicApi.Application.Requests.Playlist;

public record GetUserPlaylistsRequest(long UserId) : IRequest<List<PlaylistResponseDto>>;
