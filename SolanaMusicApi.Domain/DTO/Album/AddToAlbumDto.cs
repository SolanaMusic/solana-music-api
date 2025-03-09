using System.ComponentModel.DataAnnotations;

namespace SolanaMusicApi.Domain.DTO.Album;

public class AddToAlbumDto
{
    [Required]
    public long AlbumId { get; set; }
    [Required] 
    public long TrackId { get; set; }
}
