using SolanaMusicApi.Domain.DTO.Currency;
using SolanaMusicApi.Domain.Enums.Nft;

namespace SolanaMusicApi.Domain.DTO.Nft.Nft;

public class NftResponseDto : BaseResponseDto
{
    public string Name { get; set; } = string.Empty;
    public long CollectionId { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Owner { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Rarity Rarity { get; set; }
    public bool Available { get; set; }
    public string ImageUrl { get; set; } = string.Empty;

    public CurrencyResponseDto Currency { get; set; } = null!;
}