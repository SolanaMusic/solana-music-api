using SolanaMusicApi.Domain.Entities.Music;
using SolanaMusicApi.Domain.Entities.User;

namespace SolanaMusicApi.Domain.Entities.General;

public class RecentlyPlayed : BaseEntity
{
    public long UserId { get; set; }
    public long TrackId { get; set; }
    
    public ApplicationUser User { get; set; }  = null!;
    public Track Track { get; set; } = null!;
}