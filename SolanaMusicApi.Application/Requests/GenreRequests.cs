using MediatR;
using SolanaMusicApi.Domain.DTO.Genre;

namespace SolanaMusicApi.Application.Requests;

public record GetGenresRequest : IRequest<List<GenreResponseDto>>;
public record GetGenreRequest(long Id) : IRequest<GenreResponseDto>;
public record CreateGenreRequest(GenreRequestDto GenreRequestDto) : IRequest<GenreResponseDto>;
public record UpdateGenreRequest(long Id, GenreRequestDto GenreRequestDto) : IRequest<GenreResponseDto>;
public record DeleteGenreRequest(long Id) : IRequest<GenreResponseDto>;
