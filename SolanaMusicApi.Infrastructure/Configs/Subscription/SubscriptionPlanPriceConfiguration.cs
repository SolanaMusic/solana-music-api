using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Domain.Entities.Subscription;

namespace SolanaMusicApi.Infrastructure.Configs.Subscrioption;

public class SubscriptionPlanPriceConfiguration : IEntityTypeConfiguration<SubscriptionPlanCurrency>
{
    public void Configure(EntityTypeBuilder<SubscriptionPlanCurrency> builder)
    {
        builder.HasKey(spc => spc.Id);

        builder.Property(spc => spc.Price)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.HasOne(spc => spc.SubscriptionPlan)
            .WithMany(sp => sp.SubscriptionPlanCurrencies)
            .HasForeignKey(spc => spc.SubscriptionPlanId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(spc => spc.Currency)
            .WithMany(c => c.SubscriptionPlanCurrencies)
            .HasForeignKey(spc => spc.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
