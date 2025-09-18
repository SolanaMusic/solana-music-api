using SolanaMusicApi.Domain.DTO.Album;
using SolanaMusicApi.Domain.DTO.Artist;
using SolanaMusicApi.Domain.DTO.Currency;
using SolanaMusicApi.Domain.DTO.Nft.Nft;
using SolanaMusicApi.Domain.DTO.Track;
using SolanaMusicApi.Domain.Enums.Nft;

namespace SolanaMusicApi.Domain.DTO.Nft.NftCollection;

public class NftCollectionResponseDto : BaseResponseDto
{
    public string Name { get; set; } = string.Empty;
    public int Minted { get; set; }
    public int Supply  { get; set; }
    public decimal Price { get; set; }
    public CurrencyResponseDto Currency { get; set; } = null!;
    public string Address { get; set; } = string.Empty;
    public AssociationType AssociationType { get; set; }
    public bool IsLiked { get; set; }
    public string ImageUrl { get; set; } = string.Empty;

    public List<ArtistResponseDto> Creators { get; set; } = [];
    public List<NftResponseDto> Nfts { get; set; } = [];
    public AlbumResponseDto? Album { get; set; }
    public TrackResponseDto? Track { get; set; }
    public ArtistResponseDto? Artist { get; set; }
}