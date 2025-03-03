using MediatR;
using SolanaMusicApi.Domain.DTO.Genre;

namespace SolanaMusicApi.Application.Requests.Genre;

public record DeleteGenreRequest(long Id) : IRequest<GenreResponseDto>;
