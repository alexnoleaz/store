using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Store.Shared.Entities;
using Store.Shared.Validations;

namespace Store.Shared.Repositories.EntityFrameworkCore;

public class EntityFrameworkCoreRepositoryBase<TEntity, TPrimaryKey>
    : RepositoryBase<TEntity, TPrimaryKey>,
        IRepository<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
{
    public EntityFrameworkCoreRepositoryBase(StoreDbContext context)
    {
        ArgumentValidator.NotNull(context);
        Context = context;
    }

    public virtual StoreDbContext Context { get; }
    public virtual DbSet<TEntity> Table => Context.Set<TEntity>();
    public virtual DatabaseFacade Database => Context.Database;

    #region Select/Get/Query

    public virtual IQueryable<TEntity> GetAll() => Table;

    public virtual Task<IQueryable<TEntity>> GetAllAsync() => Task.FromResult(GetAll());

    public virtual IQueryable<TEntity> GetAllIncluding(
        params Expression<Func<TEntity, object>>[] propertySelectors
    )
    {
        ArgumentValidator.NotNull(propertySelectors);

        var query = GetAll();

        if (propertySelectors is { Length: > 0 })
            foreach (var propertySelector in propertySelectors)
                query = query.Include(propertySelector);

        return query;
    }

    public virtual async Task<IQueryable<TEntity>> GetAllIncludingAsync(
        params Expression<Func<TEntity, object>>[] propertySelectors
    )
    {
        ArgumentValidator.NotNull(propertySelectors);

        var query = await GetAllAsync();
        if (propertySelectors is { Length: > 0 })
            foreach (var propertySelector in propertySelectors)
                query = query.Include(propertySelector);

        return query;
    }

    public virtual List<TEntity> GetAllList() => GetAll().ToList();

    public virtual async Task<List<TEntity>> GetAllListAsync()
    {
        var query = await GetAllAsync();
        return await query.ToListAsync();
    }

    public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
    {
        ArgumentValidator.NotNull(predicate);
        return GetAll().Where(predicate).ToList();
    }

    public virtual async Task<List<TEntity>> GetAllListAsync(
        Expression<Func<TEntity, bool>> predicate
    )
    {
        ArgumentValidator.NotNull(predicate);
        var query = await GetAllAsync();
        return await query.Where(predicate).ToListAsync();
    }

    public virtual TEntity? Get(TPrimaryKey id)
    {
        ArgumentValidator.NotNull(id!);
        return GetAll().FirstOrDefault(CreateEqualityExpressionForId(id));
    }

    public virtual async Task<TEntity?> GetAsync(TPrimaryKey id)
    {
        ArgumentValidator.NotNull(id!);
        var query = await GetAllAsync();
        return await query.FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
    }

    public virtual TEntity? Get(Expression<Func<TEntity, bool>> predicate)
    {
        ArgumentValidator.NotNull(predicate);
        return GetAll().FirstOrDefault(predicate);
    }

    public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        ArgumentValidator.NotNull(predicate);
        var query = await GetAllAsync();
        return await query.FirstOrDefaultAsync(predicate);
    }

    #endregion

    #region Insert

    public virtual TEntity Insert(TEntity entity)
    {
        ArgumentValidator.NotNull(entity);
        return Table.Add(entity).Entity;
    }

    public virtual async Task<TEntity> InsertAsync(TEntity entity)
    {
        ArgumentValidator.NotNull(entity);
        var result = await Table.AddAsync(entity);
        return result.Entity;
    }

    public virtual TPrimaryKey InsertAndGetId(TEntity entity)
    {
        ArgumentValidator.NotNull(entity);
        entity = Insert(entity);

        if (entity.IsTransient())
            Context.SaveChanges();

        return entity.Id;
    }

    public virtual async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
    {
        ArgumentValidator.NotNull(entity);
        entity = await InsertAsync(entity);

        if (entity.IsTransient())
            await Context.SaveChangesAsync();

        return entity.Id;
    }

    #endregion

    #region Update

    public virtual TEntity Update(TEntity entity)
    {
        ArgumentValidator.NotNull(entity);
        return Table.Update(entity).Entity;
    }

    public virtual Task<TEntity> UpdateAsync(TEntity entity)
    {
        ArgumentValidator.NotNull(entity);
        entity = Update(entity);
        return Task.FromResult(entity);
    }

    #endregion

    #region Delete

    public virtual void Delete(TEntity entity)
    {
        ArgumentValidator.NotNull(entity);
        Table.Remove(entity);
    }

    public virtual Task DeleteAsync(TEntity entity)
    {
        ArgumentValidator.NotNull(entity);
        Delete(entity);
        return Task.CompletedTask;
    }

    public virtual void Delete(TPrimaryKey id)
    {
        ArgumentValidator.NotNull(id!);
        var entity = Get(id);
        Delete(entity);
    }

    public virtual Task DeleteAsync(TPrimaryKey id)
    {
        ArgumentValidator.NotNull(id!);
        var entity = Get(id);
        Delete(entity);
        return Task.CompletedTask;
    }

    #endregion

    #region Aggregates

    public virtual int Count() => GetAll().Count();

    public virtual async Task<int> CountAsync()
    {
        var query = await GetAllAsync();
        return await query.CountAsync();
    }

    public virtual int Count(Expression<Func<TEntity, bool>> predicate)
    {
        ArgumentValidator.NotNull(predicate);
        return GetAll().Where(predicate).Count();
    }

    public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        ArgumentValidator.NotNull(predicate);
        var query = await GetAllAsync();
        return await query.Where(predicate).CountAsync();
    }

    public virtual long LongCount() => GetAll().LongCount();

    public virtual async Task<long> LongCountAsync()
    {
        var query = await GetAllAsync();
        return await query.LongCountAsync();
    }

    public virtual long LongCount(Expression<Func<TEntity, bool>> predicate)
    {
        ArgumentValidator.NotNull(predicate);
        return GetAll().Where(predicate).LongCount();
    }

    public virtual async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        ArgumentValidator.NotNull(predicate);
        var query = await GetAllAsync();
        return await query.Where(predicate).LongCountAsync();
    }

    #endregion

    public int SaveChanges() => Context.SaveChanges();

    public async Task<int> SaveChangesAsync() => await Context.SaveChangesAsync();
}

public class EntityFrameworkCoreRepositoryBase<TEntity>
    : EntityFrameworkCoreRepositoryBase<TEntity, int>
    where TEntity : class, IEntity
{
    public EntityFrameworkCoreRepositoryBase(StoreDbContext context)
        : base(context) { }
}
