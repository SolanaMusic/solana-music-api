using MediatR;
using SolanaMusicApi.Domain.DTO.Album;

namespace SolanaMusicApi.Application.Requests.Album;

public record DeleteAlbumRequest(long Id) : IRequest<AlbumResponseDto>;
