using SolanaMusicApi.Domain.Entities.User;

namespace SolanaMusicApi.Domain.Entities.Nft;

public class LikedNft : BaseEntity
{
    public long UserId { get; set; }
    public long? NftId { get; set; }
    public long? CollectionId { get; set; }
    
    public ApplicationUser User { get; set; } = null!;
    public Nft? Nft { get; set; }
    public NftCollection? Collection { get; set; }
}