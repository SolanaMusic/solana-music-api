using System.ComponentModel.DataAnnotations;

namespace SolanaMusicApi.Domain.DTO.Playlist;

public class AddToPlaylistDto
{
    [Required]
    public long PlaylistId { get; set; }
    [Required]
    public long TrackId { get; set; }
}
