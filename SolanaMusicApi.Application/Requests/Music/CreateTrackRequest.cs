using MediatR;
using SolanaMusicApi.Domain.DTO.Track;

namespace SolanaMusicApi.Application.Requests.Music;

public record CreateTrackRequest(TrackRequestDto TrackRequestDto) : IRequest<TrackResponseDto>;
