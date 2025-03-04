using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Domain.Entities.Music;

namespace SolanaMusicApi.Infrastructure.Configs.Music;

class TrackGenreConfiguration : IEntityTypeConfiguration<TrackGenre>
{
    public void Configure(EntityTypeBuilder<TrackGenre> builder)
    {
        builder.HasKey(tg => new { tg.TrackId, tg.GenreId });

        builder.Property(tg => tg.Id)
            .HasColumnOrder(0)
            .ValueGeneratedOnAdd();

        builder.Property(tg => tg.TrackId).HasColumnOrder(1);
        builder.Property(tg => tg.GenreId).HasColumnOrder(2);

        builder.HasOne(tg => tg.Track)
            .WithMany(t => t.TrackGenres)
            .HasForeignKey(tg => tg.TrackId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(tg => tg.Genre)
            .WithMany(g => g.TrackGenres)
            .HasForeignKey(tg => tg.GenreId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
