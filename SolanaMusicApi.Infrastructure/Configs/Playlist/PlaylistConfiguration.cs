using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlaylistEntity = SolanaMusicApi.Domain.Entities.Playlist.Playlist;

namespace SolanaMusicApi.Infrastructure.Configs.Playlist;

public class PlaylistConfiguration : IEntityTypeConfiguration<PlaylistEntity>
{
    public void Configure(EntityTypeBuilder<PlaylistEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(a => a.CoverUrl)
            .HasMaxLength(500);

        builder.HasOne(p => p.Owner)
            .WithMany(u => u.Playlists)
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
