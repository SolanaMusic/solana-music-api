using SolanaMusicApi.Domain.Entities.User;

namespace SolanaMusicApi.Domain.Entities.Performer;

public class ArtistSubscriber : BaseEntity
{
    public long ArtistId { get; set; }
    public long SubscriberId { get; set; }

    public Artist Artist { get; set; } = null!;
    public ApplicationUser Subscriber { get; set; } = null!;
}
