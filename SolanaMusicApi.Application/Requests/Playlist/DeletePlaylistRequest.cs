using MediatR;
using SolanaMusicApi.Domain.DTO.Playlist;

namespace SolanaMusicApi.Application.Requests.Playlist;

public record DeletePlaylistRequest(long Id) : IRequest<PlaylistResponseDto>;
