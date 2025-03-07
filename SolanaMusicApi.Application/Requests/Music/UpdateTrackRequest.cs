using MediatR;
using SolanaMusicApi.Domain.DTO.Track;

namespace SolanaMusicApi.Application.Requests.Music;

public record UpdateTrackRequest(long Id, TrackRequestDto TrackRequestDto) : IRequest<TrackResponseDto>;
