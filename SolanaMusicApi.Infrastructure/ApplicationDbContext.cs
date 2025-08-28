using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Domain.Entities.General;
using SolanaMusicApi.Domain.Entities.Music;
using SolanaMusicApi.Domain.Entities.Nft;
using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Domain.Entities.Playlist;
using SolanaMusicApi.Domain.Entities.Subscription;
using SolanaMusicApi.Domain.Entities.Transaction;
using SolanaMusicApi.Domain.Entities.User;
using SolanaMusicApi.Infrastructure.Configs.General;
using SolanaMusicApi.Infrastructure.Configs.Music;
using SolanaMusicApi.Infrastructure.Configs.Nft;
using SolanaMusicApi.Infrastructure.Configs.Performer;
using SolanaMusicApi.Infrastructure.Configs.Playlist;
using SolanaMusicApi.Infrastructure.Configs.Subscrioption;
using SolanaMusicApi.Infrastructure.Configs.Subscription;
using SolanaMusicApi.Infrastructure.Configs.User;

namespace SolanaMusicApi.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>(options)
{
    public DbSet<Country> Countries { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Whitelist> Whitelist { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
    public DbSet<SubscriptionPlanCurrency> SubscriptionPlanCurrencies { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<UserSubscription> UserSubscriptions { get; set; }

    public DbSet<Artist> Artists { get; set; }
    public DbSet<ArtistTrack> ArtistTracks { get; set; }
    public DbSet<ArtistAlbum> ArtistAlbums { get; set; }
    public DbSet<ArtistSubscriber> ArtistSubscribers { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<TrackGenre> TrackGenres { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<RecentlyPlayed> RecentlyPlayed { get; set; }

    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<PlaylistTrack> PlaylistTracks { get; set; }
    
    public DbSet<NftCollection> NftCollections { get; set; }
    public DbSet<Nft> Nfts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
        modelBuilder.ApplyConfiguration(new CountryConfiguration());
        modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());

        modelBuilder.ApplyConfiguration(new SubscriptionPlanConfiguration());
        modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());
        modelBuilder.ApplyConfiguration(new SubscriptionPlanCurrencyConfiguration());
        modelBuilder.ApplyConfiguration(new UserSubscriptionConfiguration());

        modelBuilder.ApplyConfiguration(new ArtistConfiguration());
        modelBuilder.ApplyConfiguration(new ArtistTrackConfiguration());
        modelBuilder.ApplyConfiguration(new ArtistAlbumConfiguration());
        modelBuilder.ApplyConfiguration(new ArtistSubscriberConfiguration());
        modelBuilder.ApplyConfiguration(new AlbumConfiguration());
        modelBuilder.ApplyConfiguration(new TrackConfiguration());
        modelBuilder.ApplyConfiguration(new TrackGenreConfiguration());
        modelBuilder.ApplyConfiguration(new GenreConfiguration());

        modelBuilder.ApplyConfiguration(new PlaylistConfiguration());
        modelBuilder.ApplyConfiguration(new PlaylistTrackConfiguration());

        modelBuilder.ApplyConfiguration(new NftCollectionConfiguration());
        modelBuilder.ApplyConfiguration(new NftConfiguration());
    }
}
