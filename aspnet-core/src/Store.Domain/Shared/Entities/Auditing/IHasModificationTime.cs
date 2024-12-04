namespace Store.Shared.Entities.Auditing;

/// <summary>
/// Defines a contract for entities that require tracking of their last modification time.
/// The <see cref="LastModificationTime"/> is automatically updated whenever the entity is modified.
/// </summary>
public interface IHasModificationTime
{
    /// <summary>
    /// Gets or sets the timestamp of the last modification made to this entity.
    /// </summary>
    DateTime? LastModificationTime { get; set; }
}
