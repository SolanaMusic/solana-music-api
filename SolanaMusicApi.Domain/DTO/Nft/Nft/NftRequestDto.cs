namespace SolanaMusicApi.Domain.DTO.Nft.Nft;

public class NftRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Owner { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}