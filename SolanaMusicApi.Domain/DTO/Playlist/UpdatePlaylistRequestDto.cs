using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SolanaMusicApi.Domain.DTO.Playlist;

public class UpdatePlaylistRequestDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public IFormFile? CoverFile { get; set; }
}
