using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolanaMusicApi.Domain.Entities.Performer;

namespace SolanaMusicApi.Infrastructure.Configs.Performer;

public class ArtistSubscriberConfiguration : IEntityTypeConfiguration<ArtistSubscriber>
{
    public void Configure(EntityTypeBuilder<ArtistSubscriber> builder)
    {
        builder.HasKey(x => new { x.ArtistId, x.SubscriberId });
        builder.HasIndex(x => x.Id).IsUnique();

        builder.Property(x => x.Id)
            .HasColumnOrder(0)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ArtistId).HasColumnOrder(1);
        builder.Property(x => x.SubscriberId).HasColumnOrder(2);

        builder.HasOne(ta => ta.Artist)
            .WithMany(t => t.ArtistSubscribers)
            .HasForeignKey(ta => ta.ArtistId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ta => ta.Subscriber)
            .WithMany(a => a.ArtistSubscribes)
            .HasForeignKey(ta => ta.SubscriberId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
