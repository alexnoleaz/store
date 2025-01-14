using Store.Shared.Reflection;

namespace Store.Shared.Entities;

/// <summary>
/// Represents an entity with a primary key of type <see cref="int"/>.
/// </summary>
public abstract class Entity : Entity<int>, IEntity { }

/// <summary>
/// Represents a base class for entities with a specified primary key type.
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
