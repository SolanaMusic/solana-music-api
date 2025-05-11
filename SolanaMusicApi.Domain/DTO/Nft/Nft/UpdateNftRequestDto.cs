using System.ComponentModel.DataAnnotations;

namespace SolanaMusicApi.Domain.DTO.Nft.Nft;

public class UpdateNftRequestDto
{
    [Required]
    public long Id { get; set; }
    
    [Required]
    public string Owner { get; set; } = string.Empty;
}
