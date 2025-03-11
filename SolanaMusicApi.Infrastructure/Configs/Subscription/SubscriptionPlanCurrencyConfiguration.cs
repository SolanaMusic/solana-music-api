using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Domain.Entities.Subscription;

namespace SolanaMusicApi.Infrastructure.Configs.Subscrioption;

public class SubscriptionPlanCurrencyConfiguration : IEntityTypeConfiguration<SubscriptionPlanCurrency>
{
    public void Configure(EntityTypeBuilder<SubscriptionPlanCurrency> builder)
    {
        builder.HasKey(pt => new { pt.SubscriptionPlanId, pt.CurrencyId});

        builder.Property(pt => pt.Id)
            .HasColumnOrder(0)
            .ValueGeneratedOnAdd();

        builder.Property(pt => pt.SubscriptionPlanId).HasColumnOrder(1);
        builder.Property(pt => pt.CurrencyId).HasColumnOrder(2);

        builder.Property(spc => spc.Price)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.HasOne(spc => spc.SubscriptionPlan)
            .WithMany(sp => sp.SubscriptionPlanCurrencies)
            .HasForeignKey(spc => spc.SubscriptionPlanId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(spc => spc.Currency)
            .WithMany(c => c.SubscriptionPlanCurrencies)
            .HasForeignKey(spc => spc.CurrencyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
