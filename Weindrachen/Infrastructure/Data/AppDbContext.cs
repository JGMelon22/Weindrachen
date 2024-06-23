using Weindrachen.Infrastructure.Configuration;
using Weindrachen.Models;

namespace Weindrachen.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Wine> Wines { get; set; }
    public DbSet<Grape> Grapes { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<GrapeWine> GrapesWines { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new WineConfiguration());
        modelBuilder.ApplyConfiguration(new GrapeConfiguration());
        modelBuilder.ApplyConfiguration(new BrandConfiguration());
        modelBuilder.ApplyConfiguration(new GrapeWineConfiguration());

        // Uncomment here if you need to seed new data
        // InitialSeeding.Seed(modelBuilder);
    }
}