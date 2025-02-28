using MediatR;
using SolanaMusicApi.Domain.DTO.Genre;

namespace SolanaMusicApi.Application.Requests.Genre;

public record CreateGenreRequest(GenreRequestDto GenreRequestDto) : IRequest<GenreResponseDto>;
