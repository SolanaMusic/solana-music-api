using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolanaMusicApi.Domain.Entities.Nft;

namespace SolanaMusicApi.Infrastructure.Configs.Nft;

public class LikedNftConfiguration : IEntityTypeConfiguration<LikedNft>
{
    public void Configure(EntityTypeBuilder<LikedNft> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(n => n.User)
            .WithMany(c => c.LikedNfts)
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(n => n.Nft)
            .WithMany(n => n.LikedNfts)
            .HasForeignKey(n => n.NftId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(n => n.Collection)
            .WithMany(n => n.LikedNfts)
            .HasForeignKey(n => n.CollectionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}