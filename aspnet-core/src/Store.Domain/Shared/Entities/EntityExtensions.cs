namespace Store.Shared.Entities;

/// <summary>
/// Provides extension methods for working with entities, particularly for soft deletion and undeleting operations.
/// </summary>
public static class EntityExtensions
{
    /// <summary>
    /// Checks if the entity is either <c>null</c> or marked as deleted.
    /// This method can be used to determine if an entity should be considered as logically deleted.
    /// </summary>
    /// <param name="entity">The entity to check.</param>
    /// <returns><c>true</c> if the entity is either <c>null</c> or is marked as deleted; otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="entity"/> is <c>null</c>.</exception>
    public static bool IsNullOrDeleted(this ISoftDelete entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        return entity is null || entity.IsDeleted;
    }

    /// <summary>
    /// Undeletes the specified entity by setting the <see cref="ISoftDelete.IsDeleted"/> property to <c>false</c>.
    /// This method restores the entity to its "active" state, effectively reversing a soft delete.
    /// </summary>
    /// <param name="entity">The entity to undelete.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="entity"/> is <c>null</c>.</exception>
    public static void UnDelete(this ISoftDelete entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        entity.IsDeleted = false;
    }
}
