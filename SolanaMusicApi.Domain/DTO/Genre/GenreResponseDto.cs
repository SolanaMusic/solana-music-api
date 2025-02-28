using SolanaMusicApi.Domain.Entities;

namespace SolanaMusicApi.Domain.DTO.Genre;

public class GenreResponseDto : BaseEntity
{
    public string Name { get; set; } = string.Empty;
}
