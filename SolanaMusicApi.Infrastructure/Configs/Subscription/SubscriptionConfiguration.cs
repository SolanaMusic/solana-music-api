using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubscriptionEntity = SolanaMusicApi.Domain.Entities.Subscription.Subscription;

namespace SolanaMusicApi.Infrastructure.Configs.Subscription;

public class SubscriptionConfiguration : IEntityTypeConfiguration<SubscriptionEntity>
{
    public void Configure(EntityTypeBuilder<SubscriptionEntity> builder)
    {
        builder.HasKey(us => us.Id);

        builder.HasOne(s => s.SubscriptionPlan)
            .WithMany(sp => sp.Subscriptions)
            .HasForeignKey(s => s.SubscriptionPlanId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Owner)
            .WithMany()
            .HasForeignKey(s => s.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.UserSubscriptions)
            .WithOne(us => us.Subscription)
            .HasForeignKey(us => us.SubscriptionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.ActiveUsers)
            .WithOne(u => u.ActiveSubscription)
            .HasForeignKey(u => u.ActiveSubscriptionId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(s => s.FamilyMembers)
            .WithMany()
            .UsingEntity(j => j.ToTable("SubscriptionFamilyMembers"));  
    }
}
