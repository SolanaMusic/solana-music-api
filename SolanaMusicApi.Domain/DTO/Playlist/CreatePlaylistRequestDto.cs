using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SolanaMusicApi.Domain.DTO.Playlist;

public class CreatePlaylistRequestDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public long OwnerId { get; set; }

    public bool IsPublic { get; set; } = true;
    public IFormFile? CoverFile { get; set; }
}
