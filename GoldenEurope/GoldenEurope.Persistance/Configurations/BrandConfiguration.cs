using GoldenEurope.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoldenEurope.Persistance.Configurations;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasIndex(b => b.Name).IsUnique();
        builder.HasMany(b => b.Models)
            .WithOne(m => m.Brand)
            .HasForeignKey(b => b.BrandId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}