using MediatR;
using SolanaMusicApi.Domain.DTO.Track;

namespace SolanaMusicApi.Application.Requests.Music;

public record DeleteTrackRequest(long Id) : IRequest<TrackResponseDto>;
