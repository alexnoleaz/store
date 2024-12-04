namespace Store.Shared.Entities;

/// <summary>
/// Represents an entity with a primary key of type <see cref="int"/>.
/// </summary>
public interface IEntity : IEntity<int> { }

/// <summary>
/// Represents an entity with a primary key of type <typeparamref name="TPrimaryKey"/>.
/// </summary>
/// <typeparam name="TPrimaryKey">The type of the primary key.</typeparam>
public interface IEntity<TPrimaryKey>
{
    /// <summary>
    /// Gets or sets the primary key of the entity.
    /// </summary>
    TPrimaryKey Id { get; set; }

    /// <summary>
    /// Determines whether the entity is transient (i.e., it has not been assigned a valid primary key).
    /// </summary>
    /// <returns><c>true</c> if the entity is transient; otherwise, <c>false</c>.</returns>
    bool IsTransient();
}
