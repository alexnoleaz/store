using Microsoft.EntityFrameworkCore;
using Store.Products;

namespace Store.Shared.Repositories.EntityFrameworkCore;

public class StoreDbContext : DbContext
{
    public StoreDbContext(DbContextOptions<StoreDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<DateTime>().HaveColumnType("date");
        configurationBuilder.Properties<string>().HaveColumnType("varchar");
        configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
    }

    public DbSet<Product> Products => Set<Product>();
}