namespace Store.Shared.Entities.Auditing;

/// <summary>
/// Represents an entity that has a deletion time.
/// </summary>
public interface IHasDeletionTime : ISoftDelete
{
    /// <summary>
    /// Gets or sets the deletion time of the entity.
    /// </summary>
    DateTime? DeletionTime { get; set; }
}
