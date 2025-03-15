using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Domain.Entities.Subscription;

namespace SolanaMusicApi.Infrastructure.Configs.Subscrioption;

public class SubscriptionPlanCurrencyConfiguration : IEntityTypeConfiguration<SubscriptionPlanCurrency>
{
    public void Configure(EntityTypeBuilder<SubscriptionPlanCurrency> builder)
    {
        builder.HasKey(x => new { x.SubscriptionPlanId, x.CurrencyId});
        builder.HasIndex(x => x.Id).IsUnique();

        builder.Property(x => x.Id)
            .HasColumnOrder(0)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.SubscriptionPlanId).HasColumnOrder(1);
        builder.Property(x => x.CurrencyId).HasColumnOrder(2);

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
