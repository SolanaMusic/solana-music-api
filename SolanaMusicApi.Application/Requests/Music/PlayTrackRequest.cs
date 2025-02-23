using MediatR;

namespace SolanaMusicApi.Application.Requests.Music;

public record PlayTrackRequest(long Id) : IRequest<FileStream>;
