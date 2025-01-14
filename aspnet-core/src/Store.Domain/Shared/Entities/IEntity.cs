namespace Store.Shared.Entities;

/// <summary>
/// Represents a basic entity with a primary key of type <see cref="int"/>.
/// </summary>
public interface IEntity : IEntity<int> { }

/// <summary>
/// Represents a basic entity with a specified primary key type.
/// </summary>
/// <typeparam name="TPrimaryKey">The type of the primary key.</typeparam>
public interface IEntity<TPrimaryKey>
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    TPrimaryKey Id { get; set; }

    /// <summary>
    /// Determines whether the entity is transient (not yet persisted).
    /// </summary>
    /// <returns><c>true</c> if the entity is transient; otherwise <c>false</c>.</returns>
    bool IsTransient();
}
