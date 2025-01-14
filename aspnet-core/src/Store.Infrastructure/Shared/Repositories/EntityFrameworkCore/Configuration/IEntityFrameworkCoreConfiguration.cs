namespace Store.Shared.Repositories.EntityFrameworkCore.Configuration;

/// <summary>
/// Interface for configuring the connection string for Entity Framework Core.
/// </summary>
public interface IEntityFrameworkCoreConfiguration
{
    /// <summary>
    /// Gets the connection string for the Entity Framework Core database context.
    /// </summary>
    string ConnectionString { get; }
}
