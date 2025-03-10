using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SolanaMusicApi.Domain.DTO.Artist;

public class ArtistRequestDto
{
    public long? UserId { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public long CountryId { get; set; }
    public string? Bio { get; set; }
    public IFormFile? ImageFile { get; set; }
}
