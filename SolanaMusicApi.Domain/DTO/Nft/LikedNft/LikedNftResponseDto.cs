using SolanaMusicApi.Domain.DTO.Nft.Nft;
using SolanaMusicApi.Domain.DTO.Nft.NftCollection;

namespace SolanaMusicApi.Domain.DTO.Nft.LikedNft;

public class LikedNftResponseDto : BaseResponseDto
{
    public long UserId { get; set; }
    public DateTime CreatedDate { get; set; }
    
    public GetNftResponseDto? Nft { get; set; }
    public NftCollectionResponseDto? Collection { get; set; }
}