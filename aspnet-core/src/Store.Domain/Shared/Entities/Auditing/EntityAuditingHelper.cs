using Store.Shared.Validations;

namespace Store.Shared.Entities.Auditing;

/// <summary>
/// Provides extension methods for entity auditing.
/// </summary>
public static class EntityAuditingHelper
{
    /// <summary>
    /// Sets the creation audit property on the given entity.
    /// </summary>
    /// <param name="entityAsObj">The entity object to audit.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="entityAsObj"/> is null.</exception>
    public static void SetCreationAuditProperty(object entityAsObj)
    {
        ArgumentNullException.ThrowIfNull(entityAsObj, nameof(entityAsObj));

        if (entityAsObj is not IHasCreationTime entity)
            return;

        if (entity.CreationTime == default)
            entity.CreationTime = DateTime.UtcNow;
    }

    /// <summary>
    /// Sets the modification audit property on the given entity.
    /// </summary>
    /// <param name="entityAsObj">The entity object to audit.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="entityAsObj"/> is null.</exception>
    public static void SetModificationAuditProperty(object entityAsObj)
    {
        ArgumentValidator.NotNull(entityAsObj);

        if (entityAsObj is not IHasModificationTime entity)
            return;

        if (
            entity.LastModificationTime is null
            || entity.LastModificationTime < DateTime.UtcNow.AddMinutes(-1)
        )
            entity.LastModificationTime = DateTime.UtcNow;
    }

    /// <summary>
    /// Sets the deletion audit property on the given entity.
    /// </summary>
    /// <param name="entityAsObj">The entity object to audit.</param>
    /// <param name="overwriteExisting">
    /// If <c>true</c>, overwrites the existing deletion time; otherwise only sets it if null.
    /// </param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="entityAsObj"/> is null.</exception>
    public static void SetDeletionAuditProperty(object entityAsObj, bool overwriteExisting = false)
    {
        ArgumentValidator.NotNull(entityAsObj);

        if (entityAsObj is not IHasDeletionTime entity)
            return;

        if (entity.DeletionTime is null || overwriteExisting)
            entity.DeletionTime = DateTime.UtcNow;
    }
}