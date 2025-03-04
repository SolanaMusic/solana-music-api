using MediatR;
using SolanaMusicApi.Domain.DTO.Track;

namespace SolanaMusicApi.Application.Requests.Music;

public record GetTrackRequest(long Id) : IRequest<TrackResponseDto>;
