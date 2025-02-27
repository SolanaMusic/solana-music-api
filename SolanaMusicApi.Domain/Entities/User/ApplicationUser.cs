using Microsoft.AspNetCore.Identity;
using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Domain.Entities.Subscription;
using SubscriptionEntity = SolanaMusicApi.Domain.Entities.Subscription.Subscription;
using PlaylistEntity = SolanaMusicApi.Domain.Entities.Playlist.Playlist;
using TransactionEntity = SolanaMusicApi.Domain.Entities.Transaction.Transaction;

namespace SolanaMusicApi.Domain.Entities.User;

public class ApplicationUser : IdentityUser<long>
{
    public long? ActiveSubscriptionId { get; set; }

    public UserProfile Profile { get; set; } = null!;
    public Artist? Artist { get; set; }
    public SubscriptionEntity? ActiveSubscription { get; set; }
    public ICollection<UserSubscription> UserSubscriptions { get; set; } = [];
    public ICollection<Artist> SubscribedArtists { get; set; } = [];
    public ICollection<PlaylistEntity> Playlists { get; set; } = [];
    public ICollection<TransactionEntity>? Transactions { get; set; }
}
