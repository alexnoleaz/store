using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Store.Shared.Validations;

namespace Store.Shared.Dependencies;

/// <summary>
/// Provides extension methods for registering dependencies in the <see cref="IServiceCollection"/>.
/// </summary>
public static class DependenciesServiceCollectionExtensions
{
    /// <summary>
    /// Registers conventional services by scanning the specified assemblies for classes implementing
    /// marker interfaces such as <see cref="IScopedDependency"/>, <see cref="ISingletonDependency"/>,
    /// and <see cref="ITransientDependency"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which services will be added.</param>
    /// <param name="assemblies">The assemblies to scan for dependencies.</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="services"/> or <paramref name="assemblies"/> is null.</exception>
    public static IServiceCollection AddConventionalServices(
        this IServiceCollection services,
        IEnumerable<Assembly> assemblies
    )
    {
        ArgumentValidator.NotNull(services);
        ArgumentValidator.NotNull(assemblies);

        foreach (var assembly in assemblies)
            ConventionalRegistrar.RegisterAssemblyByConvention(services, assembly);

        return services;
    }
}