using MediatR;

namespace SolanaMusicApi.Application.Requests.Genre;

public record DeleteGenreRequest(long Id) : IRequest<bool>;
