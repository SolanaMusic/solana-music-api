using MediatR;
using SolanaMusicApi.Domain.DTO.Playlist;

namespace SolanaMusicApi.Application.Requests.Playlist;

public record UpdatePlaylistRequest(long Id, UpdatePlaylistRequestDto PlaylistRequestDto) : IRequest<PlaylistResponseDto>;
