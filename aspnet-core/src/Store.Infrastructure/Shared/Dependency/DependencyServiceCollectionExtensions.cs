using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Store.Shared.Dependency;

/// <summary>
/// Provides extension methods for <see cref="IServiceCollection"/> to register services.
/// </summary>
public static class DependencyServiceCollectionExtensions
{
    /// <summary>
    /// Registers services from the specified assemblies by convention.
    /// Ensures that the <paramref name="services"/> collection and <paramref name="assemblies"/>
    /// are not null and that the <paramref name="assemblies"/> collection does not contain null elements.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the services will be added.</param>
    /// <param name="assemblies">A collection of <see cref="Assembly"/> objects from which services will be registered.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> with the registered services.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is null.</exception>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="assemblies"/> is null or contains null elements.</exception>
    public static IServiceCollection AddConventionalServices(
        this IServiceCollection services,
        IEnumerable<Assembly> assemblies
    )
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(assemblies, nameof(assemblies));
        if (assemblies.Any(a => a is null))
            throw new ArgumentNullException(
                nameof(assemblies),
                "The collection cannot contain null elements."
            );

        foreach (var assembly in assemblies)
            ConventionalRegistrar.RegisterAssemblyByConvention(services, assembly);

        return services;
    }
}
