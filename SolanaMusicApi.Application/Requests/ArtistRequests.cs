using MediatR;
using SolanaMusicApi.Domain.DTO.Artist;

namespace SolanaMusicApi.Application.Requests;

public record GetArtistsRequest(long UserId) : IRequest<List<ArtistResponseDto>>;
public record GetArtistsByNameRequest(string? Name) : IRequest<List<ArtistResponseDto>>;
public record GetArtistRequest(long Id, long UserId) : IRequest<ArtistResponseDto>;
public record CreateArtistRequest(ArtistRequestDto ArtistRequestDto) : IRequest<ArtistResponseDto>;
public record SubscribeToArtistRequest(SubscribeToArtistDto SubscribeToArtistDto) : IRequest;
public record UnsubscribeFromArtistRequest(long Id, long UserId) : IRequest;
public record UpdateArtistRequest(long Id, ArtistRequestDto ArtistRequestDto) : IRequest<ArtistResponseDto>;
public record DeleteArtistRequest(long Id) : IRequest<ArtistResponseDto>;
