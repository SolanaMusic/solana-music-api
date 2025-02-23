using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolanaMusicApi.Domain.Entities.Performer;

namespace SolanaMusicApi.Infrastructure.Configs.Performer;

public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Bio)
            .HasMaxLength(1000);

        builder.Property(a => a.ImageUrl)
            .HasMaxLength(500);

        builder.HasMany(a => a.Albums)
            .WithMany(a => a.Artists)
            .UsingEntity(j => j.ToTable("ArtistAlbums"));

        builder.HasMany(a => a.Tracks)
            .WithMany(t => t.Artists)
            .UsingEntity(j => j.ToTable("ArtistTracks"));
    }
}
