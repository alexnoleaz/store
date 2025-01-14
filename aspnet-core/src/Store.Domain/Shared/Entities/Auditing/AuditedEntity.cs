namespace Store.Shared.Entities.Auditing;

/// <summary>
/// Represents an entity with auditing for creation and modification times a primary key of type <see cref="int"/>.
/// </summary>
public abstract class AuditedEntity : AuditedEntity<int>, IEntity { }

/// <summary>
/// Represents an entity with auditing for creation and modification times and a specified primary key type.
/// </summary>
/// <typeparam name="TPrimaryKey">The type of the primary key.</typeparam>
public abstract class AuditedEntity<TPrimaryKey>
    : CreationAuditedEntity<TPrimaryKey>,
        IHasModificationTime
{
    /// <inheritdoc />
    public virtual DateTime? LastModificationTime { get; set; }
}
