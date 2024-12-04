namespace Store.Shared.Entities;

/// <summary>
/// Defines the functionality for soft deleting entities.
/// Soft-deleted entities are not physically removed from the database,
/// but are marked with <see cref="IsDeleted"/> set to <c>true</c>,
/// making them invisible to the application.
/// </summary>
public interface ISoftDelete
{
    /// <summary>
    /// Gets or sets a value indicating whether the entity has been soft deleted.
    /// </summary>
    bool IsDeleted { get; set; }
}