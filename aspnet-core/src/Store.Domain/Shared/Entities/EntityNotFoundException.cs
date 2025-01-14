namespace Store.Shared.Entities;

/// <summary>
/// Exception thrown when an entity cannot be found.
/// </summary>
public class EntityNotFoundException : Exception
{
    private const string DefaultMessage = "The requested entity was not found.";

    /// <summary>
    /// Gets the type of the entity that was not found.
    /// </summary>
    public Type? EntityType { get; }

    /// <summary>
    /// Gets the identifier of the entity that was not found.
    /// </summary>
    public object? Id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class with the default message.
    /// </summary>
    public EntityNotFoundException()
        : base(DefaultMessage) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class with a specified message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public EntityNotFoundException(string? message)
        : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class with a specified message and inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public EntityNotFoundException(string? message, Exception? innerException)
        : base(message, innerException) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class with a specified entity type and identifier.
    /// </summary>
    /// <param name="entityType">The type of the entity.</param>
    /// <param name="id">The identifier of the entity.</param>
    public EntityNotFoundException(Type? entityType, object? id)
        : this(entityType, id, null) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class with a specified entity type, identifier, and inner exception.
    /// </summary>
    /// <param name="entityType">The type of the entity.</param>
    /// <param name="id">The identifier of the entity.</param>
    /// <param name="innerException">The inner exception.</param>
    public EntityNotFoundException(Type? entityType, object? id, Exception? innerException)
        : base(
            $"Entity of type '{entityType?.FullName}' with ID '{id}' was not found.",
            innerException
        )
    {
        EntityType = entityType;
        Id = id;
    }
}
