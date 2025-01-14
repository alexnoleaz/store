using Store.Shared.Validations;

namespace Store.Shared.Entities;

/// <summary>
/// Provides extensions for operations on entities.
/// </summary>
public static class EntityExtensions
{
    /// <summary>
    /// Determines whether the specified entity is null or marked as deleted.
    /// </summary>
    /// <param name="entity">The entity to check.</param>
    /// <returns><c>true</c> if the entity is null or marked as deleted; otherwise <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="entity"/> is null.</exception>
    public static bool IsNullOrDeleted(this ISoftDelete entity)
    {
        ArgumentValidator.NotNull(entity);
        return entity is null || entity.IsDeleted;
    }

    /// <summary>
    /// Marks the specified entity as not deleted.
    /// </summary>
    /// <param name="entity">The entity to modify.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="entity"/> is null.</exception>
    public static void UnDelete(this ISoftDelete entity)
    {
        ArgumentValidator.NotNull(entity);
        entity.IsDeleted = false;
    }
}