using Microsoft.EntityFrameworkCore;

namespace Store.Shared.Repositories.EntityFrameworkCore;

public class StoreDbContext : DbContext
{
    public StoreDbContext(DbContextOptions options)
        : base(options) { }
}
