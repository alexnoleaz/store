using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Shared.Repositories.EntityFrameworkCore;
using Store.Shared.Repositories.EntityFrameworkCore.Configuration;
using Store.Shared.Validations;

namespace Store.Shared.Repositories;

/// <summary>
/// Provides methods for registering repositories in the dependency injection container.
/// </summary>
public static class RepositoryRegistrar
{
    /// <summary>
    /// Registers the repositories.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> where services will be registered.</param>
    /// <param name="configuration">The configuration that contains the connection string for the DbContext.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="services"/> or <paramref name="configuration"/> is null.</exception>
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        ArgumentValidator.NotNull(services);
        ArgumentValidator.NotNull(configuration);

        EntityFrameworkCoreRegistrar.Register<ApplicationDbContext>(
            services,
            new EntityFrameworkCoreConfiguration(configuration)
        );
    }
}
