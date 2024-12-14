namespace Store.Shared.Entities;

/// <summary>
/// Represents an exception that is thrown when an entity that is expected to be found is not found.
/// </summary>
public class EntityNotFoundException : Exception
{
    private const string DefaultMessage = "The requested entity was not found.";

    /// <summary>
    /// Gets the type of the entity that was not found.
    /// </summary>
    public Type? EntityType { get; }

    /// <summary>
    /// Gets the ID of the entity that was not found.
    /// </summary>
    public object? Id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
    /// </summary>
    public EntityNotFoundException()
        : base(DefaultMessage) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class with a specific error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public EntityNotFoundException(string? message)
        : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/>
    /// class with a specific error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The inner exception that is the cause of the current exception.</param>
    public EntityNotFoundException(string? message, Exception? innerException)
        : base(message, innerException) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class with the specified entity type and ID.
    /// </summary>
    /// <param name="entityType">The type of the entity that was not found.</param>
    /// <param name="id">The ID of the entity that was not found.</param>
    public EntityNotFoundException(Type? entityType, object? id)
        : this(entityType, id, null) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/>
    /// class with the specified entity type, ID, and inner exception.
    /// </summary>
    /// <param name="entityType">The type of the entity that was not found.</param>
    /// <param name="id">The ID of the entity that was not found.</param>
    /// <param name="innerException">The inner exception that is the cause of the current exception.</param>
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
