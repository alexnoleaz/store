namespace Store.Shared.Entities.Auditing;

/// <summary>
/// Represents an entity that has a modification time.
/// </summary>
public interface IHasModificationTime
{
    /// <summary>
    /// Gets or sets the last modification time of the entity.
    /// </summary>
    DateTime? LastModificationTime { get; set; }
}
