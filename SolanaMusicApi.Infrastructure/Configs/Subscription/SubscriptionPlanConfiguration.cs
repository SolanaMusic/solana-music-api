using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolanaMusicApi.Domain.Entities.Subscription;

namespace SolanaMusicApi.Infrastructure.Configs.Subscrioption;

public class SubscriptionPlanConfiguration : IEntityTypeConfiguration<SubscriptionPlan>
{
    public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
    {
        builder.HasKey(sp => sp.Id);

        builder.Property(sp => sp.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(sp => sp.DurationInMonths)
            .IsRequired();

        builder.Property(sp => sp.Type)
            .IsRequired();

        builder.Property(sp => sp.MaxMembers)
            .IsRequired();

        builder.HasMany(sp => sp.Subscriptions)
            .WithOne(us => us.SubscriptionPlan)
            .HasForeignKey(us => us.SubscriptionPlanId);
    }
}
