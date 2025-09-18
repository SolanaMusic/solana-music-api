namespace SolanaMusicApi.Domain.DTO.Nft.LikedNft;

public class LikedNftRequestDto
{
    public long UserId { get; set; }
    public long? NftId { get; set; }
    public long? CollectionId { get; set; }
    
    public void Validate()
    {
        var hasNft = NftId.HasValue;
        var hasCollection = CollectionId.HasValue;

        switch (hasNft)
        {
            case false when !hasCollection:
                throw new InvalidOperationException("Liked Nft must have either NftId or CollectionId");
            case true when hasCollection:
                throw new InvalidOperationException("Liked Nft cannot have both NftId and CollectionId at the same time");
        }
    }
}