using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Domain.Entities.General;
using SolanaMusicApi.Domain.Entities.Music;
using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Domain.Entities.Playlist;
using SolanaMusicApi.Domain.Entities.Subscription;
using SolanaMusicApi.Domain.Entities.Transaction;
using SolanaMusicApi.Domain.Entities.User;
using SolanaMusicApi.Infrastructure.Configs.General;
using SolanaMusicApi.Infrastructure.Configs.Music;
using SolanaMusicApi.Infrastructure.Configs.Performer;
using SolanaMusicApi.Infrastructure.Configs.Playlist;
using SolanaMusicApi.Infrastructure.Configs.Subscrioption;
using SolanaMusicApi.Infrastructure.Configs.Subscription;
using SolanaMusicApi.Infrastructure.Configs.User;

namespace SolanaMusicApi.Application;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Country> Countries { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<UserSubscription> UserSubscriptions { get; set; }

    public DbSet<Artist> Artists { get; set; }
    public DbSet<ArtistTrack> ArtistTracks { get; set; }
    public DbSet<ArtistAlbum> ArtistAlbums { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<TrackGenre> TrackGenres { get; set; }
    public DbSet<Genre> Genres { get; set; }

    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<PlaylistTrack> PlaylistTracks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CountryConfiguration());
        modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());

        modelBuilder.ApplyConfiguration(new SubscriptionPlanConfiguration());
        modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());
        modelBuilder.ApplyConfiguration(new UserSubscriptionConfiguration());

        modelBuilder.ApplyConfiguration(new ArtistConfiguration());
        modelBuilder.ApplyConfiguration(new ArtistTrackConfiguration());
        modelBuilder.ApplyConfiguration(new ArtistAlbumConfiguration());
        modelBuilder.ApplyConfiguration(new AlbumConfiguration());
        modelBuilder.ApplyConfiguration(new TrackConfiguration());
        modelBuilder.ApplyConfiguration(new TrackGenreConfiguration());
        modelBuilder.ApplyConfiguration(new GenreConfiguration());

        modelBuilder.ApplyConfiguration(new PlaylistConfiguration());
        modelBuilder.ApplyConfiguration(new PlaylistTrackConfiguration());
    }
}
