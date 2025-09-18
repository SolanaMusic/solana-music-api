using System.ComponentModel.DataAnnotations.Schema;
using SolanaMusicApi.Domain.Entities.Music;
using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Domain.Enums.Nft;

namespace SolanaMusicApi.Domain.Entities.Nft;

public class NftCollection : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int Supply  { get; set; }
    public string Address { get; set; } = string.Empty;
    public long AssociationId { get; set; }
    public AssociationType AssociationType { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    
    [NotMapped]
    public bool IsLiked { get; set; }

    public ICollection<Nft> Nfts { get; set; } = [];
    public Album? Album { get; set; }
    public Track? Track { get; set; }
    public Artist? Artist { get; set; }
    public ICollection<LikedNft> LikedNfts { get; set; } = [];
}