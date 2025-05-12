using SolanaMusicApi.Domain.DTO.Artist;
using SolanaMusicApi.Domain.DTO.Genre;

namespace SolanaMusicApi.Domain.DTO.Album;

public class GetTrackAlbumResponseDto : BaseResponseDto
{
    public string Title { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string? Description { get; set; }
    public long PlaysCount { get; set; }

    public ICollection<GetAlbumArtistResponseDto> Artists { get; set; } = null!;
    public ICollection<GenreResponseDto> Genres { get; set; } = null!;
}