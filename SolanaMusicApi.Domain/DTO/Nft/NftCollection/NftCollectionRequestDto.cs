using SolanaMusicApi.Domain.Enums.Nft;

namespace SolanaMusicApi.Domain.DTO.Nft.NftCollection;

public class NftCollectionRequestDto
{
    public string Name { get; set; } = string.Empty;
    public long CurrencyId { get; set; }
    public decimal Price { get; set; }
    public string Address { get; set; } = string.Empty;
    public long AssociationId { get; set; }
    public AssociationType AssociationType { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}