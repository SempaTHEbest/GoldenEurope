using GoldenEurope.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoldenEurope.Persistance.Configurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.Property(c => c.Vin)
            .IsRequired()
            .HasMaxLength(17)
            .IsFixedLength();
        builder.HasIndex(c => c.Vin).IsUnique();

        builder.Property(c => c.Price)
            .HasPrecision(18, 2);
        builder.Property(c => c.EngineVolume)
            .HasPrecision(4, 1);
        builder.Property(c => c.Description)
            .HasMaxLength(1000);
        builder.Property(c => c.Condition)
            .IsRequired();
    }
}