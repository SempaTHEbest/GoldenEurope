using GoldenEurope.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoldenEurope.Persistance.Configurations;

public class ModelConfiguration : IEntityTypeConfiguration<Model>
{
    public void Configure(EntityTypeBuilder<Model> builder)
    {
        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(255);
        builder.HasIndex(m => new {m.BrandId, m.Name}).IsUnique();
    }
}