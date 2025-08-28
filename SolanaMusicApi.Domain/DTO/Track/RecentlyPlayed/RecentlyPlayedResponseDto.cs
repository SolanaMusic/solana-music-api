using SolanaMusicApi.Domain.Entities;

namespace SolanaMusicApi.Domain.DTO.Track.RecentlyPlayed;

public class RecentlyPlayedResponseDto : BaseEntity
{
    public long UserId { get; set; }
    public long TrackId { get; set; }
}