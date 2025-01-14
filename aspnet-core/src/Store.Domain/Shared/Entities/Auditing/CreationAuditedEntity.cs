namespace Store.Shared.Entities.Auditing;

/// <summary>
/// Represents an entity with auditing for creation time a primary key of type <see cref="int"/>.
/// </summary>
public abstract class CreationAuditedEntity : CreationAuditedEntity<int>, IEntity { }

/// <summary>
/// Represents an entity with auditing for creation time and a specified primary key type.
/// </summary>
/// <typeparam name="TPrimaryKey">The type of the primary key.</typeparam>
public abstract class CreationAuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, IHasCreationTime
{
    /// <inheritdoc />
    public virtual DateTime CreationTime { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CreationAuditedEntity{TPrimaryKey}"/> class.
    /// </summary>
    protected CreationAuditedEntity() => CreationTime = DateTime.UtcNow;
}