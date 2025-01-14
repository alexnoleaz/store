namespace Store.Shared.Entities.Auditing;

/// <summary>
/// Represents an entity with full auditing capabilities, including creation, modification,
/// and deletion times a primary key of type <see cref="int"/>.
/// </summary>
public abstract class FullAuditedEntity : FullAuditedEntity<int>, IEntity { }

/// <summary>
/// Represents an entity with full auditing capabilities and a specified primary key type.
/// </summary>
/// <typeparam name="TPrimaryKey">The type of the primary key.</typeparam>
public abstract class FullAuditedEntity<TPrimaryKey> : AuditedEntity<TPrimaryKey>, IHasDeletionTime
{
    /// <inheritdoc />
    public virtual bool IsDeleted { get; set; }

    /// <inheritdoc />
    public virtual DateTime? DeletionTime { get; set; }
}
