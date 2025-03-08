using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolanaMusicApi.Domain.Entities.Performer;

namespace SolanaMusicApi.Infrastructure.Configs.Performer;

public class ArtistTrackConfiguration : IEntityTypeConfiguration<ArtistTrack>
{
    public void Configure(EntityTypeBuilder<ArtistTrack> builder)
    {
        builder.HasKey(ta => new { ta.TrackId, ta.ArtistId });

        builder.Property(ta => ta.Id)
            .HasColumnOrder(0)
            .ValueGeneratedOnAdd();

        builder.Property(ta => ta.ArtistId).HasColumnOrder(1);
        builder.Property(ta => ta.TrackId).HasColumnOrder(2);

        builder.HasOne(ta => ta.Track)
            .WithMany(t => t.ArtistTracks)
            .HasForeignKey(ta => ta.TrackId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ta => ta.Artist)
            .WithMany(a => a.ArtistTracks)
            .HasForeignKey(ta => ta.ArtistId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
