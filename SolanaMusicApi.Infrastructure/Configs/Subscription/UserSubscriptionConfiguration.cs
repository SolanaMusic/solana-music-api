using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Domain.Entities.Subscription;

namespace SolanaMusicApi.Infrastructure.Configs.Subscrioption;

public class UserSubscriptionConfiguration : IEntityTypeConfiguration<UserSubscription>
{
    public void Configure(EntityTypeBuilder<UserSubscription> builder)
    {
        builder.HasKey(x => new { x.UserId, x.SubscriptionId });
        builder.HasIndex(x => x.Id).IsUnique();

        builder.Property(x => x.Id)
            .HasColumnOrder(0)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId).HasColumnOrder(1);
        builder.Property(x => x.SubscriptionId).HasColumnOrder(2);

        builder.HasOne(us => us.User)
            .WithMany(u => u.UserSubscriptions)
            .HasForeignKey(us => us.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(us => us.Subscription)
            .WithMany(s => s.UserSubscriptions)
            .HasForeignKey(us => us.SubscriptionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}