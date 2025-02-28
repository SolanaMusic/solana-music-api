using MediatR;
using SolanaMusicApi.Domain.DTO.Genre;

namespace SolanaMusicApi.Application.Requests.Genre;

public record GetAllGenresRequest : IRequest<List<GenreResponseDto>>;
