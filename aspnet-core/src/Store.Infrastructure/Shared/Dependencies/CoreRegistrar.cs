using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Shared;
using Store.Shared.Repositories;
using Store.Shared.Validations;

namespace Store.Store.Dependencies;

/// <summary>
/// Provides methods for registering core services in the dependency injection container.
/// </summary>
public static class CoreRegistrar
{
    /// <summary>
    /// Registers the core services.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> where services will be registered.</param>
    /// <param name="configuration">The configuration that contains the connection string or other settings.</param>
    /// <param name="assemblies">The assemblies to scan.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="services"/>, <paramref name="configuration"/>, or <paramref name="assemblies"/> is null.</exception>
    public static void Register(
        IServiceCollection services,
        IConfiguration configuration,
        IEnumerable<Assembly> assemblies
    )
    {
        ArgumentValidator.NotNull(services);
        ArgumentValidator.NotNull(configuration);
        ArgumentValidator.NotNull(assemblies);

        services.AddSingleton(typeof(ILogger<>), typeof(MicrosoftLogger<>));
        services.AddAutoMapper(assemblies);
        RepositoryRegistrar.Register(services, configuration);
    }
}
