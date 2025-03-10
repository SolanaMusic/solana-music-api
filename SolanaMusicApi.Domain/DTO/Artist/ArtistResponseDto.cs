using SolanaMusicApi.Domain.DTO.Album;
using SolanaMusicApi.Domain.DTO.General.CountryDto;
using SolanaMusicApi.Domain.DTO.Track;
using SolanaMusicApi.Domain.DTO.User;

namespace SolanaMusicApi.Domain.DTO.Artist;

public class ArtistResponseDto : BaseResponseDto
{
    public string Name { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string? ImageUrl { get; set; }
    public long SubscribersCount { get; set; }
    public bool IsUserSubscribed { get; set; }

    public UserResponseDto? User { get; set; }
    public CountryResponseDto Country { get; set; } = null!;
    public ICollection<GetArtistAlbumResponseDto> Albums { get; set; } = [];
    public ICollection<GetArtistTrackResponseDto> Tracks { get; set; } = [];
}
