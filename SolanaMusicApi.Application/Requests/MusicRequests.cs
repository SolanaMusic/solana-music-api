using MediatR;
using SolanaMusicApi.Domain.DTO.Track;
using SolanaMusicApi.Domain.DTO.Track.RecentlyPlayed;

namespace SolanaMusicApi.Application.Requests;

public record GetTracksRequest : IRequest<List<TrackResponseDto>>;
public record GetTracksByArtistRequest(long ArtistId, string? Name) : IRequest<List<TrackResponseDto>>;
public record GetTrackRequest(long Id) : IRequest<TrackResponseDto>;
public record GetRecentlyPlayedRequest(long UserId) : IRequest<List<TrackResponseDto>>;
public record AddOrUpdateRecentlyPlayedRequest(long UserId, long TrackId) : IRequest<RecentlyPlayedResponseDto>;
public record StreamTrackRequest(long Id) : IRequest<FileStream>;
public record CreateTrackRequest(TrackRequestDto TrackRequestDto) : IRequest<TrackResponseDto>;
public record UpdateTrackRequest(long Id, TrackRequestDto TrackRequestDto) : IRequest<TrackResponseDto>;
public record DeleteTrackRequest(long Id) : IRequest<TrackResponseDto>;
