using SolanaMusicApi.Domain.DTO.Transaction;

namespace SolanaMusicApi.Domain.DTO.Nft.Nft;

public class MintNftRequestDto
{
    public required UpdateNftRequestDto NftRequest { get; set; }
    public required TransactionRequestDto TransactionRequest { get; set; }
}