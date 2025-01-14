namespace Store.Shared.Entities;

/// <summary>
/// Represents an entity that supports activation and deactivation.
/// </summary>
public interface IPassivable
{
    /// <summary>
    /// Gets or sets a value indicating whether the entity is active.
    /// </summary>
    bool IsActive { get; set; }
}