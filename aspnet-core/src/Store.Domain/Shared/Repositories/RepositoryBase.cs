using System.Linq.Expressions;
using Store.Shared.Entities;

namespace Store.Shared.Repositories;

/// <summary>
/// Base class to implement <see cref="IRepository{TEntity,TPrimaryKey}"/>.
/// It implements some methods in most simple way.
/// </summary>
/// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
/// <typeparam name="TPrimaryKey">Primary key of the entity</typeparam>
public abstract class RepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
{
    #region Select/Get/Query

    /// <inheritdoc/>
    public abstract IQueryable<TEntity> GetAll();

    /// <inheritdoc/>
    public abstract Task<IQueryable<TEntity>> GetAllAsync();

    /// <inheritdoc/>
    public virtual IQueryable<TEntity> GetAllIncluding(
        params Expression<Func<TEntity, object>>[] propertySelectors
    ) => GetAll();

    /// <inheritdoc/>
    public virtual Task<IQueryable<TEntity>> GetAllIncludingAsync(
        params Expression<Func<TEntity, object>>[] propertySelectors
    ) => GetAllAsync();

    /// <inheritdoc/>
    public virtual List<TEntity> GetAllList() => GetAll().ToList();

    /// <inheritdoc/>
    public virtual Task<List<TEntity>> GetAllListAsync() => Task.FromResult(GetAllList());

    /// <inheritdoc/>
    public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate) =>
        GetAll().Where(predicate).ToList();

    /// <inheritdoc/>
    public virtual Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate) =>
        Task.FromResult(GetAllList(predicate));

    /// <inheritdoc/>
    public virtual TEntity Get(TPrimaryKey id)
    {
        var entity = FirstOrDefault(id);
        if (entity is null)
            throw new EntityNotFoundException(typeof(TEntity), id!);

        return entity;
    }

    /// <inheritdoc/>
    public virtual async Task<TEntity> GetAsync(TPrimaryKey id)
    {
        var entity = await FirstOrDefaultAsync(id);
        if (entity is null)
            throw new EntityNotFoundException(typeof(TEntity), id!);

        return entity;
    }

    /// <inheritdoc/>
    public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate) =>
        GetAll().Single(predicate);

    /// <inheritdoc/>
    public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate) =>
        Task.FromResult(Single(predicate));

    /// <inheritdoc/>
    public virtual TEntity? FirstOrDefault(TPrimaryKey id) =>
        GetAll().FirstOrDefault(CreateEqualityExpressionForId(id));

    /// <inheritdoc/>
    public virtual Task<TEntity?> FirstOrDefaultAsync(TPrimaryKey id) =>
        Task.FromResult(FirstOrDefault(id));

    /// <inheritdoc/>
    public virtual TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate) =>
        GetAll().FirstOrDefault(predicate);

    /// <inheritdoc/>
    public virtual Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) =>
        Task.FromResult(FirstOrDefault(predicate));

    #endregion

    #region Insert

    /// <inheritdoc/>
    public abstract TEntity Insert(TEntity entity);

    /// <inheritdoc/>
    public virtual Task<TEntity> InsertAsync(TEntity entity) => Task.FromResult(Insert(entity));

    #endregion

    #region Update

    /// <inheritdoc/>
    public abstract TEntity Update(TEntity entity);

    /// <inheritdoc/>
    public virtual Task<TEntity> UpdateAsync(TEntity entity) => Task.FromResult(Update(entity));

    /// <inheritdoc/>
    public virtual TEntity Update(TPrimaryKey id, Action<TEntity> updateAction)
    {
        var entity = Get(id);
        updateAction(entity);
        return entity;
    }

    /// <inheritdoc/>
    public virtual async Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction)
    {
        var entity = await GetAsync(id);
        await updateAction(entity);
        return entity;
    }

    #endregion

    #region Delete

    /// <inheritdoc/>
    public abstract void Delete(TEntity entity);

    /// <inheritdoc/>
    public virtual Task DeleteAsync(TEntity entity)
    {
        Delete(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public abstract void Delete(TPrimaryKey id);

    /// <inheritdoc/>
    public virtual Task DeleteAsync(TPrimaryKey id)
    {
        Delete(id);
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
    {
        foreach (var entity in GetAllList(predicate))
            Delete(entity);
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = await GetAllListAsync(predicate);

        foreach (var entity in entities)
            await DeleteAsync(entity);
    }

    #endregion

    #region Aggregates

    /// <inheritdoc/>
    public virtual int Count() => GetAll().Count();

    /// <inheritdoc/>
    public virtual Task<int> CountAsync() => Task.FromResult(Count());

    /// <inheritdoc/>
    public virtual int Count(Expression<Func<TEntity, bool>> predicate) =>
        GetAll().Count(predicate);

    /// <inheritdoc/>
    public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate) =>
        Task.FromResult(Count(predicate));

    /// <inheritdoc/>
    public virtual long LongCount() => GetAll().LongCount();

    /// <inheritdoc/>
    public virtual Task<long> LongCountAsync() => Task.FromResult(LongCount());

    /// <inheritdoc/>
    public virtual long LongCount(Expression<Func<TEntity, bool>> predicate) =>
        GetAll().LongCount(predicate);

    /// <inheritdoc/>
    public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate) =>
        Task.FromResult(LongCount(predicate));

    #endregion

    protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
    {
        var lambdaParam = Expression.Parameter(typeof(TEntity));
        var leftExpression = Expression.PropertyOrField(lambdaParam, "Id");
        var idValue = Convert.ChangeType(id, typeof(TPrimaryKey));

        Expression<Func<object>> closure = () => idValue;
        var rightExpression = Expression.Convert(closure.Body, leftExpression.Type);
        var lambdaBody = Expression.Equal(leftExpression, rightExpression);

        return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
    }
}
