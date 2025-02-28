using Microsoft.AspNetCore.Http;
using SolanaMusicApi.Domain.Enums.File;
using System.ComponentModel.DataAnnotations;

namespace SolanaMusicApi.Domain.DTO.File;

public class FileRequestDto
{
    [Required]
    public IFormFile File { get; set; } = null!;
    [Required]
    public FileTypes FileType { get; set; } = FileTypes.Unknown;
    public ImageFormats ImageFormat { get; set; }
}
