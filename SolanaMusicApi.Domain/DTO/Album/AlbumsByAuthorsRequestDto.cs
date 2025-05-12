using System.ComponentModel.DataAnnotations;

namespace SolanaMusicApi.Domain.DTO.Album;

public class AlbumsByArtistsRequestDto
{
    [Required] public List<long> ArtistIds { get; set; } = null!;
    public string? Title { get; set; }
}