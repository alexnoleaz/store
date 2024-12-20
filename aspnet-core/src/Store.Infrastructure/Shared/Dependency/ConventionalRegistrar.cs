using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Store.Shared.Reflection;

namespace Store.Shared.Dependency;

/// <summary>
/// Provides methods to register services in an <see cref="IServiceCollection"/> based on conventions.
/// </summary>
public static class ConventionalRegistrar
{
    /// <summary>
    /// Registers all services in the specified assembly following lifetime conventions.
    /// </summary>
    /// <param name="services">The service collection to register services into.</param>
    /// <param name="assembly">The assembly containing the services to be registered.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> or <paramref name="assembly"/> is null.</exception>
    public static void RegisterAssemblyByConvention(IServiceCollection services, Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(assembly, nameof(assembly));

        var types = assembly
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericTypeDefinition)
            .ToArray();

        RegisterByLifetime<IScopedDependency>(services, types, ServiceLifetime.Scoped);
        RegisterByLifetime<ITransientDependency>(services, types, ServiceLifetime.Transient);
        RegisterByLifetime<ISingletonDependency>(services, types, ServiceLifetime.Singleton);
    }

    /// <summary>
    /// Registers types that implement the specified dependency interface with the specified lifetime.
    /// </summary>
    /// <typeparam name="TDependency">The marker interface indicating the lifetime of the dependencies.</typeparam>
    /// <param name="services">The service collection to register services into.</param>
    /// <param name="types">The collection of types to be considered for registration.</param>
    /// <param name="lifetime">The lifetime to be assigned to the registered services.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> or <paramref name="types"/> is null.</exception>
    private static void RegisterByLifetime<TDependency>(
        IServiceCollection services,
        IEnumerable<Type> types,
        ServiceLifetime lifetime
    )
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(types, nameof(types));

        var markerType = TypeHelper.Get<TDependency>();
        var relevantTypes = types.Where(type => type.GetInterfaces().Contains(markerType));

        foreach (var implementationType in relevantTypes)
        {
            var interfaces = implementationType
                .GetInterfaces()
                .Where(interfaceType => interfaceType != markerType);

            foreach (var @interface in interfaces)
                services.Add(new ServiceDescriptor(@interface, implementationType, lifetime));

            if (!interfaces.Any())
                services.Add(
                    new ServiceDescriptor(implementationType, implementationType, lifetime)
                );
        }
    }
}
