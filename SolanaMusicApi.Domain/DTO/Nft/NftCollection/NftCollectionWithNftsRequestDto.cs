using SolanaMusicApi.Domain.DTO.Nft.Nft;

namespace SolanaMusicApi.Domain.DTO.Nft.NftCollection;

public class NftCollectionWithNftsRequestDto
{
    public required NftCollectionRequestDto NftCollection { get; set; }
    public List<NftRequestDto> Nfts { get; set; } = [];
}