using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Domain.Entities.Subscription;

namespace SolanaMusicApi.Infrastructure.Configs.Subscrioption;

public class UserSubscriptionConfiguration : IEntityTypeConfiguration<UserSubscription>
{
    public void Configure(EntityTypeBuilder<UserSubscription> builder)
    {
        builder.HasKey(us => new { us.UserId, us.SubscriptionId });

        builder.HasOne(us => us.User)
            .WithMany(u => u.UserSubscriptions)
            .HasForeignKey(us => us.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(us => us.Subscription)
            .WithMany(s => s.UserSubscriptions)
            .HasForeignKey(us => us.SubscriptionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}