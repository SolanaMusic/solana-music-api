using MediatR;
using SolanaMusicApi.Domain.DTO.Playlist;

namespace SolanaMusicApi.Application.Requests.Playlist;

public record RemoveFromPlaylistRequest(AddToPlaylistDto AddToPlaylistDto) : IRequest;
