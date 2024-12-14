namespace Store.Shared.Entities.Auditing;

/// <summary>
/// Provides utility methods for setting audit properties on entities, such as creation, modification, and deletion timestamps.
/// </summary>
public static class EntityAuditingHelper
{
    /// <summary>
    /// Sets the <see cref="IHasCreationTime.CreationTime"/> property to the current UTC time if it has not been previously set.
    /// </summary>
    /// <param name="entityAsObj">The entity object to audit.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="entityAsObj"/> is <c>null</c>.</exception>
    public static void SetCreationAuditProperty(object entityAsObj)
    {
        ArgumentNullException.ThrowIfNull(entityAsObj, nameof(entityAsObj));

        if (entityAsObj is not IHasCreationTime entity)
            return;

        if (entity.CreationTime == default)
            entity.CreationTime = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the <see cref="IHasModificationTime.LastModificationTime"/> property to the current UTC time
    /// if it is either not set or older than one minute.
    /// </summary>
    /// <param name="entityAsObj">The entity object to audit.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="entityAsObj"/> is <c>null</c>.</exception>
    public static void SetModificationAuditProperty(object entityAsObj)
    {
        ArgumentNullException.ThrowIfNull(entityAsObj, nameof(entityAsObj));

        if (entityAsObj is not IHasModificationTime entity)
            return;

        if (
            entity.LastModificationTime is null
            || entity.LastModificationTime < DateTime.UtcNow.AddMinutes(-1)
        )
            entity.LastModificationTime = DateTime.UtcNow;
    }

    /// <summary>
    /// Sets the <see cref="IHasDeletionTime.DeletionTime"/> property to the current UTC time.
    /// Optionally, overwrites an existing value.
    /// </summary>
    /// <param name="entityAsObj">The entity object to audit.</param>
    /// <param name="overwriteExisting">Indicates whether to overwrite the existing deletion time if it is already set.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="entityAsObj"/> is <c>null</c>.</exception>
    public static void SetDeletionAuditProperty(object entityAsObj, bool overwriteExisting = false)
    {
        ArgumentNullException.ThrowIfNull(entityAsObj, nameof(entityAsObj));

        if (entityAsObj is not IHasDeletionTime entity)
            return;

        if (entity.DeletionTime is null || overwriteExisting)
            entity.DeletionTime = DateTime.UtcNow;
    }
}