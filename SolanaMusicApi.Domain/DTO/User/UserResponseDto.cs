using Microsoft.AspNetCore.Identity;
using SolanaMusicApi.Domain.DTO.Artist.ArtistApplication;
using SolanaMusicApi.Domain.DTO.User.Profile;
using SolanaMusicApi.Domain.DTO.Nft.Nft;
using SolanaMusicApi.Domain.DTO.Playlist;
using SolanaMusicApi.Domain.DTO.Transaction;

namespace SolanaMusicApi.Domain.DTO.User;

public class UserResponseDto : IdentityUser<long>
{
    public int Following  { get; set; }
    public string Role { get; set; } = string.Empty;
    public string? ArtistName { get; set; }
    
    public UserProfileResponseDto Profile { get; set; } = null!;
    public List<NftResponseDto> Nfts { get; set; } = [];
    public List<GetUserTransactionResponseDto> Transactions { get; set; } = [];
    public List<GetUserPlaylistResponseDto> Playlists { get; set; } = [];
    public UserArtistApplicationDto? ArtistApplication  { get; set; }
}
