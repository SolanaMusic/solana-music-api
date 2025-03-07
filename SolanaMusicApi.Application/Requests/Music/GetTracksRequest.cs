using MediatR;
using SolanaMusicApi.Domain.DTO.Track;

namespace SolanaMusicApi.Application.Requests.Music;

public record GetTracksRequest : IRequest<List<TrackResponseDto>>;
