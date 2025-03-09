using MediatR;
using SolanaMusicApi.Domain.DTO.Playlist;

namespace SolanaMusicApi.Application.Requests.Playlist;

public record AddToPlaylistRequest(AddToPlaylistDto AddToPlaylistDto) : IRequest;
