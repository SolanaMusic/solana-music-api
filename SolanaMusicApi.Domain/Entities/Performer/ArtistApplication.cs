using SolanaMusicApi.Domain.Entities.User;
using SolanaMusicApi.Domain.Enums;

namespace SolanaMusicApi.Domain.Entities.Performer;

public class ArtistApplication : BaseEntity
{
    public long UserId { get; set; }
    public string ResourceUrl { get; set; } = string.Empty;
    public string ContactLink { get; set; } = string.Empty;
    public ArtistApplicationStatus Status { get; set; } = ArtistApplicationStatus.Pending;
    public long? ReviewerId { get; set; }
    
    public ApplicationUser User { get; set; } = null!;
    public ApplicationUser? Reviewer { get; set; }
}