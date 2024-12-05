using System.Linq.Expressions;
using Store.Shared.Entities;

namespace Store.Shared.Repositories;

/// <summary>
/// Abstract base class providing default implementations for <see cref="IRepository{TEntity, TPrimaryKey}"/>.
/// </summary>
/// <typeparam name="TEntity">The type of the entity managed by the repository.</typeparam>
/// <typeparam name="TPrimaryKey">The type of the primary key of the entity.</typeparam>
public abstract class RepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
{
    /// <inheritdoc />
    public abstract IQueryable<TEntity> GetAll();

    /// <inheritdoc />
    public virtual Task<IQueryable<TEntity>> GetAllAsync() => Task.FromResult(GetAll());

    /// <inheritdoc />
    public abstract IQueryable<TEntity> GetAllIncluding(
        params Expression<Func<TEntity, object>>[] propertySelectors
    );

    /// <inheritdoc />
    public virtual Task<IQueryable<TEntity>> GetAllIncludingAsync(
        params Expression<Func<TEntity, object>>[] propertySelectors
    ) => Task.FromResult(GetAllIncluding());

    /// <inheritdoc />
    public abstract List<TEntity> GetAllList();

    /// <inheritdoc />
    public virtual Task<List<TEntity>> GetAllListAsync() => Task.FromResult(GetAllList());

    /// <inheritdoc />
    public abstract List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

    /// <inheritdoc />
    public virtual Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate) =>
        Task.FromResult(GetAllList(predicate));

    /// <inheritdoc />
    public abstract TEntity Get(TPrimaryKey id);

    /// <inheritdoc />
    public virtual Task<TEntity> GetAsync(TPrimaryKey id) => Task.FromResult(Get(id));

    /// <inheritdoc />
    public abstract TEntity? Get(Expression<Func<TEntity, bool>> predicate);

    /// <inheritdoc />
    public virtual Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate) =>
        Task.FromResult(Get(predicate));

    /// <inheritdoc />
    public abstract TEntity Insert(TEntity entity);

    /// <inheritdoc />
    public virtual Task<TEntity> InsertAsync(TEntity entity) => Task.FromResult(Insert(entity));

    /// <inheritdoc />
    public abstract TPrimaryKey InsertAndGetId(TEntity entity);

    /// <inheritdoc />
    public virtual Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity) =>
        Task.FromResult(InsertAndGetId(entity));

    /// <inheritdoc />
    public abstract TEntity Update(TEntity entity);

    /// <inheritdoc />
    public virtual Task<TEntity> UpdateAsync(TEntity entity) => Task.FromResult(Update(entity));

    /// <inheritdoc />
    public abstract void Delete(TEntity entity);

    /// <inheritdoc />
    public virtual Task DeleteAsync(TEntity entity)
    {
        Delete(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public abstract void Delete(TPrimaryKey id);

    /// <inheritdoc />
    public virtual Task DeleteAsync(TPrimaryKey id)
    {
        Delete(id);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public virtual int Count() => GetAll().Count();

    /// <inheritdoc />
    public virtual Task<int> CountAsync() => Task.FromResult(Count());

    /// <inheritdoc />
    public virtual int Count(Expression<Func<TEntity, bool>> predicate) =>
        GetAll().Count(predicate);

    /// <inheritdoc />
    public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate) =>
        Task.FromResult(Count(predicate));

    /// <inheritdoc />
    public virtual long LongCount() => GetAll().LongCount();

    /// <inheritdoc />
    public virtual Task<long> LongCountAsync() => Task.FromResult(LongCount());

    /// <inheritdoc />
    public virtual long LongCount(Expression<Func<TEntity, bool>> predicate) =>
        GetAll().LongCount(predicate);

    /// <inheritdoc />
    public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate) =>
        Task.FromResult(LongCount(predicate));

    /// <summary>
    /// Creates an equality expression to compare entity identifiers.
    /// </summary>
    /// <param name="id">The identifier to compare.</param>
    /// <returns>An expression that compares the entity's Id with the given identifier.</returns>
    protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
    {
        var lambdaParam = Expression.Parameter(typeof(TEntity));
        var leftExpression = Expression.PropertyOrField(lambdaParam, "Id");
        var idValue = Convert.ChangeType(id, typeof(TPrimaryKey));

        Expression<Func<object>> closure = () => idValue!;
        var rightExpression = Expression.Convert(closure.Body, leftExpression.Type);
        var lambdaBody = Expression.Equal(leftExpression, rightExpression);

        return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
    }
}
