using SolanaMusicApi.Domain.DTO.Track;
using SolanaMusicApi.Domain.DTO.User;

namespace SolanaMusicApi.Domain.DTO.Playlist;

public class PlaylistResponseDto : BaseResponseDto
{
    public string Name { get; set; } = string.Empty;
    public string? CoverUrl { get; set; }

    public UserResponseDto Owner { get; set; } = null!;
    public ICollection<GetAlbumTrackResponseDto>? Tracks { get; set; }
}
