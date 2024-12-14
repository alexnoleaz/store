using Store.Shared.Reflection;

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
    public virtual TPrimaryKey Id { get; set; } = default!;

    /// <inheritdoc />
    public virtual bool IsTransient()
    {
        if (EqualityComparer<TPrimaryKey>.Default.Equals(Id, default(TPrimaryKey)))
            return true;

        return TypeHelper.Get<TPrimaryKey>() switch
        {
            Type t when t == TypeHelper.Get<int>() => Convert.ToInt32(Id) <= 0,
            Type t when t == TypeHelper.Get<long>() => Convert.ToInt64(Id) <= 0,
            _ => false,
        };
    }
}
