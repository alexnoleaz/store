namespace Store.Shared.Entities;

/// <summary>
/// Represents an entity that supports logical deletion.
/// </summary>
public interface ISoftDelete
{
    /// <summary>
    /// Gets or sets a value indicating whether the entity is deleted.
    /// </summary>
    bool IsDeleted { get; set; }
}