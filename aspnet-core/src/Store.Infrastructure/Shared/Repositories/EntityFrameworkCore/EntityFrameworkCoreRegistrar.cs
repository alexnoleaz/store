using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Store.Shared.Entities;
using Store.Shared.Reflection;
using Store.Shared.Repositories.EntityFrameworkCore.Configuration;
using Store.Shared.Validations;

namespace Store.Shared.Repositories.EntityFrameworkCore;

/// <summary>
/// Provides methods for registering Entity Framework Core-related services in the dependency injection container.
/// </summary>
public static class EntityFrameworkCoreRegistrar
{
    /// <summary>
    /// Registers the DbContext and repositories.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the DbContext to register.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> where services will be registered.</param>
    /// <param name="configuration">The configuration that contains the connection string for the DbContext.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="services"/> or <paramref name="configuration"/> is null.</exception>
    public static void Register<TDbContext>(
        IServiceCollection services,
        IEntityFrameworkCoreConfiguration configuration
    )
        where TDbContext : DbContext
    {
        ArgumentValidator.NotNull(services);
        ArgumentValidator.NotNull(configuration);

        services.AddDbContext<TDbContext>(options =>
            options.UseSqlServer(configuration.ConnectionString)
        );

        var type = TypeHelper.Get<Entity>();
        var entityTypes = type
            .Assembly.GetTypes()
            .Where(t => type.IsAssignableFrom(t) && !t.IsAbstract);

        foreach (var entityType in entityTypes)
        {
            var dbContextType = TypeHelper.Get<TDbContext>();
            var repositoryTypeSingle = typeof(IRepository<>).MakeGenericType(entityType);
            var implementationTypeSingle =
                typeof(EntityFrameworkCoreRepositoryBase<,>).MakeGenericType(
                    dbContextType,
                    entityType
                );
            services.AddScoped(repositoryTypeSingle, implementationTypeSingle);

            var primaryKeyProperty = entityType.GetProperties().FirstOrDefault(p => p.Name == "Id");
            if (primaryKeyProperty is not null)
            {
                var primaryKeyType = primaryKeyProperty.PropertyType;
                var repositoryTypeMulti = typeof(IRepository<,>).MakeGenericType(
                    entityType,
                    primaryKeyType
                );
                var implementationTypeMulti =
                    typeof(EntityFrameworkCoreRepositoryBase<,,>).MakeGenericType(
                        dbContextType,
                        entityType,
                        primaryKeyType
                    );
                services.AddScoped(repositoryTypeMulti, implementationTypeMulti);
            }
        }
    }
}