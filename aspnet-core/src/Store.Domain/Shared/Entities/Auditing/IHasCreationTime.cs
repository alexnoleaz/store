namespace Store.Shared.Entities.Auditing;

/// <summary>
/// Represents an entity that has a creation time.
/// </summary>
public interface IHasCreationTime
{
    /// <summary>
    /// Gets or sets the creation time of the entity.
    /// </summary>
    DateTime CreationTime { get; set; }
}
