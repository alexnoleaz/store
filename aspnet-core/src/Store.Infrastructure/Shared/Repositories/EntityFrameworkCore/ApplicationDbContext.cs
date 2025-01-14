using Microsoft.EntityFrameworkCore;

namespace Store.Shared.Repositories.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options) { }
}
