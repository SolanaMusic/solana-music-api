using MediatR;
using SolanaMusicApi.Domain.DTO.Album;

namespace SolanaMusicApi.Application.Requests.Album;

public record UpdateAlbumRequest(long Id, AlbumRequestDto AlbumRequestDto) : IRequest<AlbumResponseDto>;
