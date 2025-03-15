using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubscriptionEntity = SolanaMusicApi.Domain.Entities.Subscription.Subscription;

namespace SolanaMusicApi.Infrastructure.Configs.Subscription;

public class SubscriptionConfiguration : IEntityTypeConfiguration<SubscriptionEntity>
{
    public void Configure(EntityTypeBuilder<SubscriptionEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => new { x.OwnerId, x.SubscriptionPlanId }).IsUnique();

        builder.HasOne(s => s.SubscriptionPlan)
            .WithMany(sp => sp.Subscriptions)
            .HasForeignKey(s => s.SubscriptionPlanId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Owner)
            .WithMany()
            .HasForeignKey(s => s.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
