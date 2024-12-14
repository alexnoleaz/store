namespace Store.Shared.Entities.Auditing;

/// <summary>
/// Extends <see cref="ISoftDelete"/> to include a timestamp indicating when the entity was soft deleted.
/// Implement this interface if you need to store the deletion time of an entity.
/// <see cref="DeletionTime"/> is automatically set when the entity is soft deleted.
/// </summary>
public interface IHasDeletionTime : ISoftDelete
{
    /// <summary>
    /// Gets or sets the timestamp when the entity was soft deleted.
    /// </summary>
    DateTime? DeletionTime { get; set; }
}
