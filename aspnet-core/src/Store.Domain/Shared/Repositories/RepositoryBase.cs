using System.Linq.Expressions;
using Store.Shared.Entities;
using Store.Shared.Reflection;
using Store.Shared.Validations;

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
    public abstract Task<IQueryable<TEntity>> GetAllAsync();

    /// <inheritdoc />
    public virtual IQueryable<TEntity> GetAllIncluding(
        params Expression<Func<TEntity, object>>[] propertySelectors
    )
    {
        ArgumentValidator.NotNull(propertySelectors);
        return GetAll();
    }

    /// <inheritdoc />
    public virtual Task<IQueryable<TEntity>> GetAllIncludingAsync(
        params Expression<Func<TEntity, object>>[] propertySelectors
    )
    {
        ArgumentValidator.NotNull(propertySelectors);
        return GetAllAsync();
    }

    /// <inheritdoc />
    public virtual List<TEntity> GetAllList() => GetAll().ToList();

    /// <inheritdoc />
    public virtual async Task<List<TEntity>> GetAllListAsync() =>
        await Task.FromResult(GetAllList());

    /// <inheritdoc />
    public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
    {
        ArgumentValidator.NotNull(predicate);
        return GetAll().Where(predicate).ToList();
    }

    /// <inheritdoc />
    public virtual async Task<List<TEntity>> GetAllListAsync(
        Expression<Func<TEntity, bool>> predicate
    ) => await Task.FromResult(GetAllList(predicate));

    /// <inheritdoc />
    public virtual TEntity Get(TPrimaryKey id)
    {
        var entity = GetAll().FirstOrDefault(CreateEqualityExpressionForId(id));
        if (entity is null)
            throw new EntityNotFoundException(TypeHelper.Get<TEntity>(), id);

        return entity;
    }

    /// <inheritdoc />
    public virtual async Task<TEntity> GetAsync(TPrimaryKey id)
    {
        var query = await GetAllAsync();
        var entity = query.FirstOrDefault(CreateEqualityExpressionForId(id));

        if (entity is null)
            throw new EntityNotFoundException(TypeHelper.Get<TEntity>(), id);

        return entity;
    }

    /// <inheritdoc />
    public virtual TEntity? Get(Expression<Func<TEntity, bool>> predicate)
    {
        ArgumentValidator.NotNull(predicate);
        return GetAll().FirstOrDefault(predicate);
    }

    /// <inheritdoc />
    public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate) =>
        await Task.FromResult(Get(predicate));

    /// <inheritdoc />
    public abstract TEntity Insert(TEntity entity);

    /// <inheritdoc />
    public virtual async Task<TEntity> InsertAsync(TEntity entity) =>
        await Task.FromResult(Insert(entity));

    /// <inheritdoc />
    public virtual TPrimaryKey InsertAndGetId(TEntity entity) => Insert(entity).Id;

    /// <inheritdoc />
    public virtual async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
    {
        var insertedEntity = await InsertAsync(entity);
        return insertedEntity.Id;
    }

    /// <inheritdoc />
    public abstract TEntity Update(TEntity entity);

    /// <inheritdoc />
    public virtual async Task<TEntity> UpdateAsync(TEntity entity) =>
        await Task.FromResult(Update(entity));

    /// <inheritdoc />
    public abstract void Delete(TEntity entity);

    /// <inheritdoc />
    public virtual async Task DeleteAsync(TEntity entity)
    {
        Delete(entity);
        await Task.CompletedTask;
    }

    /// <inheritdoc />
    public abstract void Delete(TPrimaryKey id);

    /// <inheritdoc />
    public virtual async Task DeleteAsync(TPrimaryKey id)
    {
        Delete(id);
        await Task.CompletedTask;
    }

    /// <inheritdoc />
    public virtual int Count() => GetAll().Count();

    /// <inheritdoc />
    public virtual async Task<int> CountAsync() => await Task.FromResult(Count());

    /// <inheritdoc />
    public virtual int Count(Expression<Func<TEntity, bool>> predicate)
    {
        ArgumentValidator.NotNull(predicate);
        return GetAll().Count(predicate);
    }

    /// <inheritdoc />
    public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate) =>
        await Task.FromResult(Count(predicate));

    /// <inheritdoc />
    public virtual long LongCount() => GetAll().LongCount();

    /// <inheritdoc />
    public virtual async Task<long> LongCountAsync() => await Task.FromResult(LongCount());

    /// <inheritdoc />
    public virtual long LongCount(Expression<Func<TEntity, bool>> predicate)
    {
        ArgumentValidator.NotNull(predicate);
        return GetAll().LongCount(predicate);
    }

    /// <inheritdoc />
    public virtual async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate) =>
        await Task.FromResult(LongCount(predicate));

    /// <inheritdoc />
    public abstract void SaveChanges();

    /// <inheritdoc />
    public abstract Task SaveChangesAsync();

    /// <summary>
    /// Creates a lambda expression to compare the 'Id' property of the entity with the provided value.
    /// </summary>
    /// <param name="id">The Id value to compare.</param>
    /// <returns>A lambda expression that checks if the entity's Id is equal to the provided value.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="id"/> is null.</exception>
    protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
    {
        ArgumentValidator.NotNull(id!);

        var lambdaParam = Expression.Parameter(typeof(TEntity));
        var leftExpression = Expression.PropertyOrField(lambdaParam, "Id");
        var rightExpression = Expression.Constant(id, leftExpression.Type);
        var lambdaBody = Expression.Equal(leftExpression, rightExpression);

        return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
    }
}
