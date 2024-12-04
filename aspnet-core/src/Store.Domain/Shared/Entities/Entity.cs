namespace Store.Shared.Entities;

/// <summary>
/// Base class for entities with a primary key of type <see cref="int"/>.
/// Implements <see cref="IEntity"/>.
/// </summary>
public abstract class Entity : Entity<int>, IEntity { }

/// <summary>
/// Base class for entities with a primary key of type <typeparamref name="TPrimaryKey"/>.
/// Implements <see cref="IEntity{TPrimaryKey}"/> and provides common functionality for entities.
/// </summary>
/// <typeparam name="TPrimaryKey">The type of the primary key.</typeparam>
public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
{
    /// <inheritdoc />
    public virtual required TPrimaryKey Id { get; set; }

    /// <inheritdoc />
    public virtual bool IsTransient()
    {
        if (EqualityComparer<TPrimaryKey>.Default.Equals(Id, default(TPrimaryKey)))
            return true;

        if (typeof(TPrimaryKey) == typeof(int))
            return Convert.ToInt32(Id) <= 0;

        if (typeof(TPrimaryKey) == typeof(long))
            return Convert.ToInt64(Id) <= 0;

        return false;
    }
}
