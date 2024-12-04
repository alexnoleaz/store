namespace Store.Shared.Entities;

/// <summary>
/// Defines the functionality for marking an entity as active or inactive.
/// Entities implementing this interface can be flagged as either active or inactive.
/// </summary>
public interface IPassivable
{
    /// <summary>
    /// Gets or sets a value indicating whether the entity is active.
    /// </summary>
    bool IsActive { get; set; }
}