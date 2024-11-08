using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Store.Shared.Repositories.EntityFrameworkCore;

public static class EFCoreConfigurer
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StoreDbContext>(options =>
            options.UseSqlServer(new EFCoreConfiguration(configuration).ConnectionString)
        );
        services.AddTransient(typeof(IRepository<>), typeof(EFCoreRepositoryBase<>));
        services.AddTransient(typeof(IRepository<,>), typeof(EFCoreRepositoryBase<,>));
    }
}
