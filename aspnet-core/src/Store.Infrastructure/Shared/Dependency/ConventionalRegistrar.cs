using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Store.Shared.Validations;

namespace Store.Shared.Dependency;

public static class ConventionalRegistrar
{
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

    private static void RegisterByLifetime<TDependency>(
        IServiceCollection services,
        IEnumerable<Type> types,
        ServiceLifetime lifetime
    )
    {
        ArgumentValidator.NotNull(services);
        ArgumentValidator.NotNull(types);

        var markerType = typeof(TDependency);
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
