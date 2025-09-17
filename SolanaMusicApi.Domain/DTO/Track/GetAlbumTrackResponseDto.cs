using SolanaMusicApi.Domain.DTO.Album;
using SolanaMusicApi.Domain.DTO.Artist;
using SolanaMusicApi.Domain.DTO.Genre;

namespace SolanaMusicApi.Domain.DTO.Track;

public class GetAlbumTrackResponseDto : BaseResponseDto
{
    public string Title { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; }
    public long PlaysCount { get; set; }

    public ICollection<GenreResponseDto> Genres { get; set; } = null!;
    public GetTrackAlbumResponseDto? Album { get; set; }
    public ICollection<GetAlbumArtistResponseDto> Artists { get; set; } = null!;
}