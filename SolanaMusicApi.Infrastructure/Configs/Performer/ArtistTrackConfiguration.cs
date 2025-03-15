using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolanaMusicApi.Domain.Entities.Performer;

namespace SolanaMusicApi.Infrastructure.Configs.Performer;

public class ArtistTrackConfiguration : IEntityTypeConfiguration<ArtistTrack>
{
    public void Configure(EntityTypeBuilder<ArtistTrack> builder)
    {
        builder.HasKey(x => new { x.TrackId, x.ArtistId });
        builder.HasIndex(x => x.Id).IsUnique();

        builder.Property(x => x.Id)
            .HasColumnOrder(0)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ArtistId).HasColumnOrder(1);
        builder.Property(x => x.TrackId).HasColumnOrder(2);

        builder.HasOne(at => at.Track)
            .WithMany(t => t.ArtistTracks)
            .HasForeignKey(at => at.TrackId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(at => at.Artist)
            .WithMany(a => a.ArtistTracks)
            .HasForeignKey(at => at.ArtistId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
