namespace Store.Shared.Entities.Auditing;

/// <summary>
/// A base class for entities with a creation audit property, using <see cref="int"/> as the primary key type.
/// Inherits from <see cref="CreationAuditedEntity{TPrimaryKey}"/>.
/// </summary>
public abstract class CreationAuditedEntity : CreationAuditedEntity<int>, IEntity { }

/// <summary>
/// Provides a base implementation for entities that require tracking of their creation time.
/// Implements <see cref="IHasCreationTime"/> to automatically manage the <see cref="CreationTime"/> property.
/// </summary>
/// <typeparam name="TPrimaryKey">The type of the primary key for the entity.</typeparam>
public abstract class CreationAuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, IHasCreationTime
{
    /// <inheritdoc />
    public virtual DateTime CreationTime { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CreationAuditedEntity{TPrimaryKey}"/> class
    /// with the <see cref="CreationTime"/> property set to the current UTC time.
    /// </summary>
    protected CreationAuditedEntity() => CreationTime = DateTime.UtcNow;
}
