using MediatR;
using SolanaMusicApi.Domain.DTO.Genre;

namespace SolanaMusicApi.Application.Requests.Genre;

public record GetGenreRequest(long Id) : IRequest<GenreResponseDto>;
