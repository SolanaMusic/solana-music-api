namespace SolanaMusicApi.Domain.DTO.Track.RecentlyPlayed;

public class RecentlyPlayedRequestDto
{
    public long UserId { get; set; }
    public long TrackId { get; set; }
}