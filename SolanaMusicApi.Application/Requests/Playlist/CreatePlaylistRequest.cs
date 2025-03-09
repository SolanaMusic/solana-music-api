using MediatR;
using SolanaMusicApi.Domain.DTO.Playlist;

namespace SolanaMusicApi.Application.Requests.Playlist;

public record CreatePlaylistRequest(CreatePlaylistRequestDto PlaylistRequestDto) : IRequest<PlaylistResponseDto>;