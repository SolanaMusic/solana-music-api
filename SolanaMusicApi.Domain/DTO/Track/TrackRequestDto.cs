using Microsoft.AspNetCore.Http;
using SolanaMusicApi.Domain.Attributes;
using System.ComponentModel.DataAnnotations;

namespace SolanaMusicApi.Domain.DTO.Track;

[AlbumOrArtistRequired]
public class TrackRequestDto
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public long? AlbumId { get; set; }
    [Required]
    public DateTime ReleaseDate { get; set; }
    [Required]
    public List<long> ArtistIds { get; set; } = null!;
    [Required]
    public List<long> GenreIds { get; set; } = null!;
    public IFormFile? Image { get; set; }

    [Required(ErrorMessage = "File is required")]
    public IFormFile TrackFile { get; set; } = null!;
}
