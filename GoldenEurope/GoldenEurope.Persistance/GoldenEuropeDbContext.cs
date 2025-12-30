using GoldenEurope.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoldenEurope.Persistance;

public class GoldenEuropeDbContext : DbContext
{
    public GoldenEuropeDbContext(DbContextOptions<GoldenEuropeDbContext> options) : base(options) { }
    
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Car> Cars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GoldenEuropeDbContext).Assembly);
    }
}