namespace Store.Shared.Entities.Auditing;

/// <summary>
/// A base class for entities with auditing functionality, using <see cref="int"/> as the primary key type.
/// Inherits from <see cref="AuditedEntity{TPrimaryKey}"/>.
/// </summary>
public abstract class AuditedEntity : AuditedEntity<int>, IEntity { }

/// <summary>
/// Provides a base implementation for entities that require tracking of both creation and modification times.
/// Inherits from <see cref="CreationAuditedEntity{TPrimaryKey}"/> and implements <see cref="IHasModificationTime"/>.
/// </summary>
/// <typeparam name="TPrimaryKey">The type of the primary key for the entity.</typeparam>
public abstract class AuditedEntity<TPrimaryKey>
    : CreationAuditedEntity<TPrimaryKey>,
        IHasModificationTime
{
    /// <inheritdoc />
    public virtual DateTime? LastModificationTime { get; set; }
}
