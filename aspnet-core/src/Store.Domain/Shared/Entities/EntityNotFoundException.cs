namespace Store.Shared.Entities;

/// <summary>
/// Represents an exception that is thrown when an entity that is expected to be found is not found.
/// </summary>
public class EntityNotFoundException : Exception
{
    /// <summary>
    /// Gets or sets the type of the entity that was not found.
    /// </summary>
    public Type EntityType { get; set; }

    /// <summary>
    /// Gets or sets the ID of the entity that was not found.
    /// </summary>
    public object Id { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class with the specified entity type and ID.
    /// </summary>
    /// <param name="entityType">The type of the entity that was not found.</param>
    /// <param name="id">The ID of the entity that was not found.</param>
    public EntityNotFoundException(Type entityType, object id)
        : this(entityType, id, null) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class with the specified entity type, ID, and inner exception.
    /// </summary>
    /// <param name="entityType">The type of the entity that was not found.</param>
    /// <param name="id">The ID of the entity that was not found.</param>
    /// <param name="innerException">The inner exception that is the cause of the current exception.</param>
    public EntityNotFoundException(Type entityType, object id, Exception? innerException)
        : base(
            $"Entity of type '{entityType.FullName}' with ID '{id}' was not found.",
            innerException
        )
    {
        EntityType = entityType ?? throw new ArgumentNullException(nameof(entityType));
        Id = id ?? throw new ArgumentNullException(nameof(id));
    }
}
