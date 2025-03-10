using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolanaMusicApi.Domain.Entities.Performer;

namespace SolanaMusicApi.Infrastructure.Configs.Performer;

public class ArtistSubscriberConfiguration : IEntityTypeConfiguration<ArtistSubscriber>
{
    public void Configure(EntityTypeBuilder<ArtistSubscriber> builder)
    {
        builder.HasKey(ta => new { ta.ArtistId, ta.SubscriberId });

        builder.Property(ta => ta.Id)
            .HasColumnOrder(0)
            .ValueGeneratedOnAdd();

        builder.Property(ta => ta.ArtistId).HasColumnOrder(1);
        builder.Property(ta => ta.SubscriberId).HasColumnOrder(2);

        builder.HasOne(ta => ta.Artist)
            .WithMany(t => t.ArtistSubscribers)
            .HasForeignKey(ta => ta.ArtistId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ta => ta.Subscriber)
            .WithMany(a => a.ArtistSubscribers)
            .HasForeignKey(ta => ta.SubscriberId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
