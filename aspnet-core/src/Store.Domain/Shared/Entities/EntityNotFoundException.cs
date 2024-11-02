namespace Store.Shared.Entities;

public class EntityNotFoundException : Exception
{
    public Type EntityType { get; set; } = null!;
    public object Id { get; set; } = null!;

    public EntityNotFoundException() { }

    public EntityNotFoundException(Type entityType, object id)
        : this(entityType, id, null!) { }

    public EntityNotFoundException(Type entityType, object id, Exception innerException)
        : base(
            $"There is no such an entity. Entity type: {entityType.FullName}, id: {id}",
            innerException
        )
    {
        EntityType = entityType;
        Id = id;
    }

    public EntityNotFoundException(string message)
        : base(message) { }

    public EntityNotFoundException(string message, Exception innerException)
        : base(message, innerException) { }
}
