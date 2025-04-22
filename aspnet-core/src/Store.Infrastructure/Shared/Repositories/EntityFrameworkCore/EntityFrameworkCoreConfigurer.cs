using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Shared.Repositories.EntityFrameworkCore.Configuration;
using Store.Shared.Validations;

namespace Store.Shared.Repositories.EntityFrameworkCore;

public static class EntityFrameworkCoreConfigurer
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        ArgumentValidator.NotNull(services);
        IEntityFrameworkCoreConfiguration efCoreConfiguration =
            EntityFrameworkCoreConfiguration.GetInstance(configuration);

        services.AddDbContext<StoreDbContext>(options =>
            options.UseSqlServer(efCoreConfiguration.GetConnectionString())
        );

        services.AddScoped(typeof(IRepository<,>), typeof(EntityFrameworkCoreRepositoryBase<,>));
        services.AddScoped(typeof(IRepository<>), typeof(EntityFrameworkCoreRepositoryBase<>));
    }
}
