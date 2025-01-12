using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Store.Shared.Reflection;
using Store.Shared.Validations;

namespace Store.Shared.Dependencies;

/// <summary>
/// Provides methods for registering dependencies based on conventions.
/// </summary>
public static class ConventionalRegistrar
{
    /// <summary>
    /// Registers all types in the specified assembly that implement marker interfaces
    /// for dependency lifetimes (<see cref="IScopedDependency"/>, <see cref="ISingletonDependency"/>, <see cref="ITransientDependency"/>).
    /// </summary>
    /// <param name="services">The service collection to register dependencies into.</param>
    /// <param name="assembly">The assembly containing the types to register.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="services"/> or <paramref name="assembly"/> is null.</exception>
    public static void RegisterAssemblyByConvention(IServiceCollection services, Assembly assembly)
    {
        ArgumentValidator.NotNull(services);
        ArgumentValidator.NotNull(assembly);

        var types = assembly
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericTypeDefinition)
            .ToList();

        RegisterByLifetime<IScopedDependency>(services, types, ServiceLifetime.Scoped);
        RegisterByLifetime<ITransientDependency>(services, types, ServiceLifetime.Transient);
        RegisterByLifetime<ISingletonDependency>(services, types, ServiceLifetime.Singleton);
    }

    /// <summary>
    /// Registers all types in the specified collection that implement the specified dependency marker interface.
    /// </summary>
    /// <typeparam name="TDependency">The marker interface type.</typeparam>
    /// <param name="services">The service collection to register dependencies into.</param>
    /// <param name="types">The collection of types to evaluate and register.</param>
    /// <param name="lifetime">The lifetime to assign to the registered dependencies.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="services"/> or <paramref name="types"/> is null.</exception>
    private static void RegisterByLifetime<TDependency>(
        IServiceCollection services,
        IEnumerable<Type> types,
        ServiceLifetime lifetime
    )
    {
        ArgumentValidator.NotNull(services);
        ArgumentValidator.NotNull(types);

        var markerType = TypeHelper.Get<TDependency>();
        var relevantTypes = types.Where(t => t.GetInterfaces().Contains(markerType));

        foreach (var implementationType in relevantTypes)
        {
            var serviceInterfaces = implementationType
                .GetInterfaces()
                .Where(interfaceType => interfaceType != markerType);

            foreach (var serviceInterface in serviceInterfaces)
                services.Add(new ServiceDescriptor(serviceInterface, implementationType, lifetime));
            services.Add(new ServiceDescriptor(implementationType, implementationType, lifetime));
        }
    }
}
