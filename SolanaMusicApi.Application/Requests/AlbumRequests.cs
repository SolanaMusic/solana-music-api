using MediatR;
using SolanaMusicApi.Domain.DTO.Album;

namespace SolanaMusicApi.Application.Requests;

public record GetAlbumsRequest : IRequest<List<AlbumResponseDto>>;
public record GetAlbumsByArtistRequest(List<long> ArtistIds, string? Title) : IRequest<List<AlbumResponseDto>>;
public record GetAlbumsByArtistsRequest(List<long> ArtistIds, string? Title) : IRequest<List<AlbumResponseDto>>;
public record GetAlbumRequest(long Id) : IRequest<AlbumResponseDto>;
public record CreateAlbumRequest(AlbumRequestDto AlbumRequestDto) : IRequest<AlbumResponseDto>;
public record UpdateAlbumRequest(long Id, AlbumRequestDto AlbumRequestDto) : IRequest<AlbumResponseDto>;
public record AddToAlbumRequest(AddToAlbumDto AddToAlbumDto) : IRequest;
public record RemoveFromAlbumRequest(long TrackId) : IRequest;
public record DeleteAlbumRequest(long Id) : IRequest<AlbumResponseDto>;
