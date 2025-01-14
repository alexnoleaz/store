using Microsoft.Extensions.Configuration;
using Store.Shared.Validations;

namespace Store.Shared.Repositories.EntityFrameworkCore.Configuration;

/// <summary>
/// An implementation of the <see cref="IEntityFrameworkCoreConfiguration"/> interface.
/// Responsible for providing the connection string from the application's configuration.
/// </summary>
public class EntityFrameworkCoreConfiguration : IEntityFrameworkCoreConfiguration
{
    /// <inheritdoc />
    public string ConnectionString { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="EntityFrameworkCoreConfiguration"/>.
    /// Retrieves the connection string from the provided <paramref name="configuration"/> object.
    /// </summary>
    /// <param name="configuration">The configuration object, typically injected via dependency injection.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="configuration"/> is null.</exception>
    public EntityFrameworkCoreConfiguration(IConfiguration configuration)
    {
        ArgumentValidator.NotNull(configuration);
        ConnectionString = configuration.GetRequiredSection("ConnectionStrings:Default").Value!;
    }
}
