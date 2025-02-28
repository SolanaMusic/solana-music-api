using MediatR;
using SolanaMusicApi.Domain.DTO.Genre;

namespace SolanaMusicApi.Application.Requests.Genre;

public record UpdateGenreRequest(long Id, GenreRequestDto GenreRequestDto) : IRequest<GenreResponseDto>;
