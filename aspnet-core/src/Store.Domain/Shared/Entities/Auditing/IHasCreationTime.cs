namespace Store.Shared.Entities.Auditing;

/// <summary>
/// Defines a contract for entities that require tracking of their creation time.
/// The <see cref="CreationTime"/> is automatically set when the entity is first saved to the database.
/// </summary>
public interface IHasCreationTime
{
    /// <summary>
    /// Gets or sets the timestamp when this entity was created.
    /// </summary>
    DateTime CreationTime { get; set; }
}
