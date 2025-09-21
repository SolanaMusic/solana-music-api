using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolanaMusicApi.Domain.Entities.Performer;

namespace SolanaMusicApi.Infrastructure.Configs.Performer;

public class ArtistApplicationConfiguration : IEntityTypeConfiguration<ArtistApplication>
{
    public void Configure(EntityTypeBuilder<ArtistApplication> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.CreatedDate);

        builder.Property(x => x.ResourceUrl)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.ContactLink)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasOne(x => x.User)
            .WithOne(u => u.ArtistApplication)
            .HasForeignKey<ArtistApplication>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.Reviewer)
            .WithMany(x => x.ReviewedApplications)
            .HasForeignKey(x => x.ReviewerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}