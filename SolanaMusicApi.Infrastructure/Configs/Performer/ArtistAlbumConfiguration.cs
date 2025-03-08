using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolanaMusicApi.Domain.Entities.Performer;

namespace SolanaMusicApi.Infrastructure.Configs.Performer;

public class ArtistAlbumConfiguration : IEntityTypeConfiguration<ArtistAlbum>
{
    public void Configure(EntityTypeBuilder<ArtistAlbum> builder)
    {
        builder.HasKey(x => new { x.ArtistId, x.AlbumId });

        builder.Property(x => x.Id)
            .HasColumnOrder(0)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ArtistId).HasColumnOrder(1);
        builder.Property(x => x.AlbumId).HasColumnOrder(2);

        builder.HasOne(x => x.Artist)
            .WithMany(t => t.ArtistAlbums)
            .HasForeignKey(ta => ta.ArtistId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ta => ta.Album)
            .WithMany(a => a.ArtistAlbums)
            .HasForeignKey(ta => ta.AlbumId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
