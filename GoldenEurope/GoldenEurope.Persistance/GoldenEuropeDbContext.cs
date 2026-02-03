using GoldenEurope.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoldenEurope.Persistance;

public class GoldenEuropeDbContext : IdentityDbContext<ApplicationUser>
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