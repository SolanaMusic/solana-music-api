using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Domain.Entities.Music;

namespace SolanaMusicApi.Infrastructure.Configs.Music;

public class TrackConfiguration : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.FileUrl)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(t => t.ImageUrl)
            .HasMaxLength(500);

        builder.Property(t => t.PlaysCount)
            .HasDefaultValue(0);

        builder.Property(t => t.Duration)
            .IsRequired();

        builder.Property(t => t.ReleaseDate)
            .IsRequired();

        builder.HasOne(t => t.Album)
            .WithMany(a => a.Tracks)
            .HasForeignKey(t => t.AlbumId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(t => t.Genres)
            .WithMany(g => g.Tracks)
            .UsingEntity(j => j.ToTable("TrackGenres"));
    }
}
