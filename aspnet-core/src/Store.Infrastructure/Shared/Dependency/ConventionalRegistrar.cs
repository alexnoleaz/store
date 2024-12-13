using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Store.Shared.Reflection;

namespace Store.Shared.Dependency;

/// <summary>
/// Provides methods to automatically register classes into an <see cref="IServiceCollection"/>
/// by convention, based on predefined dependency marker interfaces.
/// </summary>
public static class ConventionalRegistrar
{
    /// <summary>
    /// Registers all types in the specified assembly that implement one or more predefined
    /// dependency marker interfaces (<see cref="IScopedDependency"/>,
    /// <see cref="ITransientDependency"/>, or <see cref="ISingletonDependency"/>).
    /// Each type is registered by its implemented interfaces and also by its concrete type.
    /// </summary>
    /// <param name="services">The service collection to register the services into.</param>
    /// <param name="assembly">The assembly containing the types to inspect and register.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="services"/> or <paramref name="assembly"/> is <c>null</c>.
    /// </exception>
    public static void RegisterAssemblyByConvention(IServiceCollection services, Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(assembly, nameof(assembly));

        var types = assembly
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericTypeDefinition);
        RegisterByLifetime<IScopedDependency>(services, types, ServiceLifetime.Scoped);
        RegisterByLifetime<ITransientDependency>(services, types, ServiceLifetime.Transient);
        RegisterByLifetime<ISingletonDependency>(services, types, ServiceLifetime.Singleton);
    }

    /// <summary>
    /// Registers all types that implement the specified marker interface (<typeparamref name="TDependency"/>)
    /// into the provided <see cref="IServiceCollection"/>. Each type is registered with its implemented
    /// interfaces, except for the marker interface, and also by its concrete type.
    /// </summary>
    /// <typeparam name="TDependency">The dependency marker interface used to filter types for registration.</typeparam>
    /// <param name="services">The service collection to register the services into.</param>
    /// <param name="types">The collection of types to inspect and register.</param>
    /// <param name="lifetime">The lifetime to assign to the registered services.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="services"/> or <paramref name="types"/> is <c>null</c>.
    /// </exception>
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

            services.Add(new ServiceDescriptor(implementationType, implementationType, lifetime));
        }
    }
}
