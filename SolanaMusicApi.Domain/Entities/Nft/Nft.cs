using SolanaMusicApi.Domain.Entities.Transaction;
using SolanaMusicApi.Domain.Entities.User;
using SolanaMusicApi.Domain.Enums.Nft;

namespace SolanaMusicApi.Domain.Entities.Nft;

public class Nft : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public long CollectionId { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Owner { get; set; } = string.Empty;
    public long? UserId { get; set; }
    public decimal Price { get; set; }
    public long CurrencyId { get; set; }
    public Rarity Rarity { get; set; }
    public string ImageUrl { get; set; } = string.Empty;

    public Currency Currency { get; set; } = null!;
    public NftCollection Collection { get; set; } = null!;
    public ApplicationUser? User { get; set; }
}