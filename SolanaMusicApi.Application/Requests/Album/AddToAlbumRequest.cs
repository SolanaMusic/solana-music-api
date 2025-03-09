using MediatR;
using SolanaMusicApi.Domain.DTO.Album;

namespace SolanaMusicApi.Application.Requests.Album;

public record AddToAlbumRequest(AddToAlbumDto AddToAlbumDto) : IRequest;
