using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Store.Shared.Repositories.EntityFrameworkCore.Configuration;
using Store.Shared.Validations;

namespace Store.Shared.Repositories.EntityFrameworkCore;

public static class EntityFrameworkCoreConfigurer
{
    public static void Configure(IServiceCollection services)
    {
        ArgumentValidator.NotNull(services);
        var serviceProvider = services.BuildServiceProvider();

        services.AddDbContext<StoreDbContext>(options =>
            options.UseSqlServer(
                serviceProvider
                    .GetRequiredService<IEntityFrameworkCoreConfiguration>()
                    .GetConnectionString()
            )
        );

        services.AddScoped(typeof(IRepository<,>), typeof(EntityFrameworkCoreRepositoryBase<,>));
        services.AddScoped(typeof(IRepository<>), typeof(EntityFrameworkCoreRepositoryBase<>));
    }
}
