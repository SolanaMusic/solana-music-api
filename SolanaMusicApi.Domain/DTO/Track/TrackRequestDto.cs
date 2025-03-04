using Microsoft.AspNetCore.Http;
using SolanaMusicApi.Domain.Attributes;
using System.ComponentModel.DataAnnotations;

namespace SolanaMusicApi.Domain.DTO.Track;

public class TrackRequestDto
{
    [Required]
    [AlbumOrArtistRequired]
    public string Title { get; set; } = string.Empty;
    public long? AlbumId { get; set; }
    [Required]
    public DateTime ReleaseDate { get; set; }

    public List<long>? ArtistIds { get; set; } = [];
    [Required]
    public List<long> GenreIds { get; set; } = [];
    public IFormFile? Image { get; set; }

    [Required(ErrorMessage = "File is required")]
    public IFormFile TrackFile { get; set; } = null!;
}
