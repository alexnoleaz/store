using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Store.Shared.Dependency;

/// <summary>
/// Provides a mechanism to register services in the <see cref="IServiceCollection"/>
/// by convention, scanning assemblies for implementations of specific dependency
/// marker interfaces.
/// </summary>
public class ConventionalServiceRegistrar
{
    private readonly IServiceCollection _services;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConventionalServiceRegistrar"/> class.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> where the services will be registered.</param>
    public ConventionalServiceRegistrar(IServiceCollection services) => _services = services;

    /// <summary>
    /// Registers all services in the given assembly that implement dependency marker interfaces
    /// (<see cref="IScopedDependency"/>, <see cref="ITransientDependency"/>, and <see cref="ISingletonDependency"/>).
    /// </summary>
    /// <param name="assembly">The assembly to scan for types to register.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="assembly"/> is <c>null</c>.</exception>
    public void RegisterAssemblyByConvention(Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);
        var types = assembly
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericTypeDefinition);

        RegisterByLifetime<IScopedDependency>(types, ServiceLifetime.Scoped);
        RegisterByLifetime<ITransientDependency>(types, ServiceLifetime.Transient);
        RegisterByLifetime<ISingletonDependency>(types, ServiceLifetime.Singleton);
    }

    /// <summary>
    /// Registers services based on their lifetime by scanning for types implementing a specific
    /// dependency marker interface.
    /// </summary>
    /// <typeparam name="TDependency">The dependency marker interface to look for
    /// (<see cref="IScopedDependency"/>, <see cref="ITransientDependency"/>, or <see cref="ISingletonDependency"/>).</typeparam>
    /// <param name="types">The collection of types to evaluate for registration.</param>
    /// <param name="lifetime">The service lifetime for the registered types.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="types"/> collection is <c>null</c>.</exception>
    private void RegisterByLifetime<TDependency>(IEnumerable<Type> types, ServiceLifetime lifetime)
    {
        ArgumentNullException.ThrowIfNull(types);

        var relevantTypes = types.Where(type => type.GetInterfaces().Contains(typeof(TDependency)));

        foreach (var implementationType in relevantTypes)
        {
            var interfaces = implementationType
                .GetInterfaces()
                .Where(interfaceType => interfaceType != typeof(TDependency));

            foreach (var @interface in interfaces)
                _services.Add(new ServiceDescriptor(@interface, implementationType, lifetime));

            _services.Add(new ServiceDescriptor(implementationType, implementationType, lifetime));
        }
    }
}