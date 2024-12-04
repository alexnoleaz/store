namespace Store.Shared.Entities.Auditing;

/// <summary>
/// A base class for fully audited entities, using <see cref="int"/> as the primary key type.
/// Inherits from <see cref="FullAuditedEntity{TPrimaryKey}"/>.
/// </summary>
public abstract class FullAuditedEntity : FullAuditedEntity<int>, IEntity { }

/// <summary>
/// Provides a base implementation for entities that require full auditing,
/// including creation, modification, and soft deletion information.
/// Inherits from <see cref="AuditedEntity{TPrimaryKey}"/> and implements <see cref="IHasDeletionTime"/>.
/// </summary>
/// <typeparam name="TPrimaryKey">The type of the primary key for the entity.</typeparam>
public abstract class FullAuditedEntity<TPrimaryKey> : AuditedEntity<TPrimaryKey>, IHasDeletionTime
{
    /// <inheritdoc />
    public virtual bool IsDeleted { get; set; }

    /// <inheritdoc />
    public virtual DateTime? DeletionTime { get; set; }
}
