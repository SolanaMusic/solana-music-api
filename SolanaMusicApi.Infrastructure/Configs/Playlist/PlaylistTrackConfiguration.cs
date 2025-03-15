using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolanaMusicApi.Domain.Entities.Playlist;

namespace SolanaMusicApi.Infrastructure.Configs.Playlist;

public class PlaylistTrackConfiguration : IEntityTypeConfiguration<PlaylistTrack>
{
    public void Configure(EntityTypeBuilder<PlaylistTrack> builder)
    {
        builder.HasKey(x => new { x.PlaylistId, x.TrackId });
        builder.HasIndex(x => x.Id).IsUnique();

        builder.Property(x => x.Id)
            .HasColumnOrder(0)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.PlaylistId).HasColumnOrder(1);
        builder.Property(x => x.TrackId).HasColumnOrder(2);

        builder.HasOne(pt => pt.Playlist)
            .WithMany(p => p.PlaylistTracks)
            .HasForeignKey(pt => pt.PlaylistId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pt => pt.Track)
            .WithMany(t => t.PlaylistTracks)
            .HasForeignKey(pt => pt.TrackId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
