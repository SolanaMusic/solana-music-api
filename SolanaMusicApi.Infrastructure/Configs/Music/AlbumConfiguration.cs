using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolanaMusicApi.Domain.Entities.Music;

namespace SolanaMusicApi.Infrastructure.Configs.Music;

public class AlbumConfiguration : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.ReleaseDate)
            .IsRequired();

        builder.Property(a => a.ImageUrl)
            .HasMaxLength(500);

        builder.Property(a => a.Description)
            .HasMaxLength(1000);

        builder.HasMany(t => t.Genres)
            .WithMany(g => g.Albums)
            .UsingEntity(j => j.ToTable("AlbumGenres"));
    }
}
