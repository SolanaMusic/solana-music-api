using MediatR;
using SolanaMusicApi.Domain.DTO.Playlist;

namespace SolanaMusicApi.Application.Requests.Playlist;

public record GetPlaylistRequest(long Id) : IRequest<PlaylistResponseDto>;
