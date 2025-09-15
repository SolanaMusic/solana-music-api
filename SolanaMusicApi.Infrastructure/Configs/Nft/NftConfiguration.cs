using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SolanaMusicApi.Infrastructure.Configs.Nft;

public class NftConfiguration : IEntityTypeConfiguration<Domain.Entities.Nft.Nft>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Nft.Nft> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(n => n.Price)
            .HasColumnType("decimal(18, 6)");

        builder.HasOne(n => n.Collection)
            .WithMany(c => c.Nfts)
            .HasForeignKey(n => n.CollectionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(n => n.User)
            .WithMany(c => c.Nfts)
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
