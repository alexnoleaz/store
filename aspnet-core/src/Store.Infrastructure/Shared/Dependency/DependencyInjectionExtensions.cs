using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Store.Shared.Dependency;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddServicesFromAssembly(
        this IServiceCollection services,
        Assembly assembly
    )
    {
        ArgumentNullException.ThrowIfNull(assembly);
        var types = assembly
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericTypeDefinition);

        return services
            .RegisterByLifetime<IScopedDependency>(types, ServiceLifetime.Scoped)
            .RegisterByLifetime<ITransientDependency>(types, ServiceLifetime.Transient)
            .RegisterByLifetime<ISingletonDependency>(types, ServiceLifetime.Singleton);
    }

    private static IServiceCollection RegisterByLifetime<TDependency>(
        this IServiceCollection services,
        IEnumerable<Type> types,
        ServiceLifetime lifetime
    )
    {
        ArgumentNullException.ThrowIfNull(types);
        var relevantTypes = types.Where(type => type.GetInterfaces().Contains(typeof(TDependency)));

        foreach (var implementationType in relevantTypes)
        {
            var serviceInterfaces = implementationType
                .GetInterfaces()
                .Where(interfaceType => interfaceType != typeof(TDependency));

            foreach (var serviceInterface in serviceInterfaces)
                services.Add(new ServiceDescriptor(serviceInterface, implementationType, lifetime));

            services.Add(new ServiceDescriptor(implementationType, implementationType, lifetime));
        }

        return services;
    }
}
