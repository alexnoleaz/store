using Microsoft.EntityFrameworkCore;

namespace Store.Shared.Repositories.EntityFrameworkCore;

public class StoreDbContext : DbContext
{
    public StoreDbContext(DbContextOptions options)
        : base(options) { }

    // Define a DbSet for each entity of the application.
}
