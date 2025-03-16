using MediatR;
using SolanaMusicApi.Domain.DTO.Playlist;

namespace SolanaMusicApi.Application.Requests;

public record GetUserPlaylistsRequest(long UserId) : IRequest<List<PlaylistResponseDto>>;
public record GetPlaylistRequest(long Id) : IRequest<PlaylistResponseDto>;
public record CreatePlaylistRequest(CreatePlaylistRequestDto PlaylistRequestDto) : IRequest<PlaylistResponseDto>;
public record AddToPlaylistRequest(AddToPlaylistDto AddToPlaylistDto) : IRequest;
public record RemoveFromPlaylistRequest(AddToPlaylistDto AddToPlaylistDto) : IRequest;
public record UpdatePlaylistRequest(long Id, UpdatePlaylistRequestDto PlaylistRequestDto) : IRequest<PlaylistResponseDto>;
public record DeletePlaylistRequest(long Id) : IRequest<PlaylistResponseDto>;
