using MediatR;
using SolanaMusicApi.Domain.DTO.Album;

namespace SolanaMusicApi.Application.Requests.Album;

public record GetAlbumRequest(long Id) : IRequest<AlbumResponseDto>;
