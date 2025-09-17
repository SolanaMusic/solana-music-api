using Microsoft.AspNetCore.Http;

namespace SolanaMusicApi.Domain.DTO.Playlist;

public class UpdatePlaylistRequestDto
{
    public string Name { get; set; } = string.Empty;
    public bool IsPublic { get; set; } = true;
    public bool RemoveCover { get; set; }
    public IFormFile? CoverFile { get; set; }
}
