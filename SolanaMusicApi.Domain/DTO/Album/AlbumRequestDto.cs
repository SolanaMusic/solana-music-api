using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SolanaMusicApi.Domain.DTO.Album;

public class AlbumRequestDto
{
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public List<long> ArtistIds { get; set; } = null!;
    [Required]
    public DateTime ReleaseDate { get; set; }
    [Required]
    public IFormFile Cover { get; set; } = null!;
    public string? Description { get; set; }
    public List<long>? TrackIds { get; set; }
}
