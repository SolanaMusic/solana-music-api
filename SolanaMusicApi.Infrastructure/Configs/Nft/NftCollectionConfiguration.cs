using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolanaMusicApi.Domain.Entities.Nft;

namespace SolanaMusicApi.Infrastructure.Configs.Nft;

public class NftCollectionConfiguration : IEntityTypeConfiguration<NftCollection>
{
    public void Configure(EntityTypeBuilder<NftCollection> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasMany(c => c.Nfts)
            .WithOne(n => n.Collection)
            .HasForeignKey(n => n.CollectionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(c => c.Album)
            .WithMany()
            .HasForeignKey(c => c.AssociationId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(c => c.Track)
            .WithMany()
            .HasForeignKey(c => c.AssociationId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(c => c.Artist)
            .WithMany()
            .HasForeignKey(c => c.AssociationId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
