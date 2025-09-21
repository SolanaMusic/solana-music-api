using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolanaMusicApi.Domain.Entities.General;

namespace SolanaMusicApi.Infrastructure.Configs.General;

public class RecentlyPlayedConfiguration : IEntityTypeConfiguration<RecentlyPlayed>
{
    public void Configure(EntityTypeBuilder<RecentlyPlayed> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(rp => new { rp.UserId, rp.TrackId }).IsUnique();
        builder.HasIndex(x => x.UpdatedDate);

        builder.HasOne(rp => rp.User)
            .WithMany(u => u.RecentlyPlayedTracks)
            .HasForeignKey(rp => rp.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(rp => rp.Track)
            .WithMany(t => t.RecentlyPlayedTracks)
            .HasForeignKey(rp => rp.TrackId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}