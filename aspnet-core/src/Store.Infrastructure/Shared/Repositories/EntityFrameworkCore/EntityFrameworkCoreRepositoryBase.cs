using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Store.Shared.Entities;
using Store.Shared.Entities.Auditing;
using Store.Shared.Validations;

namespace Store.Shared.Repositories.EntityFrameworkCore;

/// <summary>
/// A shortcut for <see cref="EntityFrameworkCoreRepositoryBase{TDbContext, TEntity, TPrimaryKey}"/> where the primary key type is <see cref="int"/>.
/// </summary>
/// <typeparam name="TDbContext">The type of the DbContext to use for database operations.</typeparam>
/// <typeparam name="TEntity">The type of the entity to be managed by the repository.</typeparam>
public class EntityFrameworkCoreRepositoryBase<TDbContext, TEntity>
    : EntityFrameworkCoreRepositoryBase<TDbContext, TEntity, int>,
        IRepository<TEntity>
    where TEntity : class, IEntity
    where TDbContext : DbContext
{
    public EntityFrameworkCoreRepositoryBase(TDbContext context)
        : base(context) { }
}

/// <summary>
/// An implmentation of <see cref="IRepository{TEntity, TPrimaryKey}"/> for Entity Framework Core.
/// </summary>
/// <typeparam name="TDbContext">The type of the DbContext to use for database operations.</typeparam>
/// <typeparam name="TEntity">The type of the entity to be managed by the repository.</typeparam>
/// <typeparam name="TPrimaryKey">The type of the primary key of the entity.</typeparam>
public class EntityFrameworkCoreRepositoryBase<TDbContext, TEntity, TPrimaryKey>
    : RepositoryBase<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
    where TDbContext : DbContext
{
    private readonly TDbContext Context;

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityFrameworkCoreRepositoryBase{TDbContext, TEntity}"/> class.
    /// </summary>
    /// <param name="context">The instance of the <see cref="TDbContext"/> to be used for database operations.</param>
    /// <exception cref="ArgumentNullException">Thrown when the provided <paramref name="context"/> is null.</exception>
    public EntityFrameworkCoreRepositoryBase(TDbContext context)
    {
        ArgumentValidator.NotNull(context);
        Context = context;
    }

    /// <summary>
    /// Gets the <see cref="DbSet{TEntity}"/> associated with the repository's entity type.
    /// </summary>
    protected virtual DbSet<TEntity> Table => Context.Set<TEntity>();

    /// <summary>
    /// Gets the <see cref="DatabaseFacade"/> associated with the current DbContext.
    /// </summary>
    protected virtual DatabaseFacade Database => Context.Database;

    /// <inheritdoc />
    public override IQueryable<TEntity> GetAll() => Table;

    /// <inheritdoc />
    public override Task<IQueryable<TEntity>> GetAllAsync() => Task.FromResult(GetAll());

    /// <inheritdoc />
    public override IQueryable<TEntity> GetAllIncluding(
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

    /// <inheritdoc />
    public override async Task<IQueryable<TEntity>> GetAllIncludingAsync(
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

    /// <inheritdoc />
    public override async Task<List<TEntity>> GetAllListAsync()
    {
        var query = await GetAllAsync();
        return await query.ToListAsync();
    }

    /// <inheritdoc />
    public override async Task<List<TEntity>> GetAllListAsync(
        Expression<Func<TEntity, bool>> predicate
    )
    {
        ArgumentValidator.NotNull(predicate);

        var query = await GetAllAsync();
        return await query.Where(predicate).ToListAsync();
    }

    /// <inheritdoc />
    public override async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        ArgumentValidator.NotNull(predicate);

        var query = await GetAllAsync();
        return await query.FirstOrDefaultAsync(predicate);
    }

    /// <inheritdoc />
    public override TEntity Insert(TEntity entity)
    {
        ArgumentValidator.NotNull(entity);
        SetCreationAuditProperty(entity);

        return Table.Add(entity).Entity;
    }

    /// <inheritdoc />
    public override async Task<TEntity> InsertAsync(TEntity entity)
    {
        ArgumentValidator.NotNull(entity);
        SetCreationAuditProperty(entity);

        return (await Table.AddAsync(entity)).Entity;
    }

    /// <inheritdoc />
    public override TPrimaryKey InsertAndGetId(TEntity entity)
    {
        entity = Insert(entity);
        if (entity.IsTransient())
            Context.SaveChanges();

        return entity.Id;
    }

    /// <inheritdoc />
    public override async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
    {
        entity = await InsertAsync(entity);
        if (entity.IsTransient())
            await Context.SaveChangesAsync();

        return entity.Id;
    }

    /// <inheritdoc />
    public override TEntity Update(TEntity entity)
    {
        ArgumentValidator.NotNull(entity);
        AttachIfNot(entity);
        SetModificationAuditProperty(entity);

        Context.Entry(entity).State = EntityState.Modified;
        return entity;
    }

    /// <inheritdoc />
    public override async Task<TEntity> UpdateAsync(TEntity entity)
    {
        entity = Update(entity);
        return await Task.FromResult(entity);
    }

    /// <inheritdoc />
    public override void Delete(TEntity entity)
    {
        AttachIfNot(entity);
        SetDeletionAuditProperty(entity);
        Update(entity);
    }

    /// <inheritdoc />
    public override void Delete(TPrimaryKey id)
    {
        var entity = GetFromChangeTrackerOrNull(id);
        if (entity is not null)
        {
            Delete(entity);
            return;
        }

        entity = Get(CreateEqualityExpressionForId(id));
        if (entity is not null)
        {
            Delete(entity);
            return;
        }
    }

    /// <inheritdoc />
    public override async Task<int> CountAsync()
    {
        var query = await GetAllAsync();
        return await query.CountAsync();
    }

    /// <inheritdoc />
    public override async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        ArgumentValidator.NotNull(predicate);

        var query = await GetAllAsync();
        return await query.CountAsync(predicate);
    }

    /// <inheritdoc />
    public override async Task<long> LongCountAsync()
    {
        var query = await GetAllAsync();
        return await query.LongCountAsync();
    }

    /// <inheritdoc />
    public override async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        ArgumentValidator.NotNull(predicate);

        var query = await GetAllAsync();
        return await query.LongCountAsync(predicate);
    }

    /// <inheritdoc />
    public override void SaveChanges() => Context.SaveChanges();

    /// <inheritdoc />
    public override async Task SaveChangesAsync() => await Context.SaveChangesAsync();

    /// <summary>
    /// Sets the creation audit property on the given entity.
    /// </summary>
    /// <param name="entityAsObj">The entity object to audit.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="entityAsObj"/> is null.</exception>
    protected void SetCreationAuditProperty(object entityAsObj) =>
        EntityAuditingHelper.SetCreationAuditProperty(entityAsObj);

    /// <summary>
    /// Sets the modification audit property on the given entity.
    /// </summary>
    /// <param name="entityAsObj">The entity object to audit.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="entityAsObj"/> is null.</exception>
    protected void SetModificationAuditProperty(object entityAsObj) =>
        EntityAuditingHelper.SetModificationAuditProperty(entityAsObj);

    /// <summary>
    /// Sets the deletion audit property on the given entity.
    /// </summary>
    /// <param name="entityAsObj">The entity object to audit.</param>
    /// <param name="overwriteExisting">
    /// If <c>true</c>, overwrites the existing deletion time; otherwise only sets it if null.
    /// </param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="entityAsObj"/> is null.</exception>
    protected void SetDeletionAuditProperty(object entityAsObj, bool overwriteExisting = false) =>
        EntityAuditingHelper.SetDeletionAuditProperty(entityAsObj, overwriteExisting);

    /// <summary>
    /// Attaches the given entity to the DbSet if it is not already tracked or is detached.
    /// </summary>
    /// <param name="entity">The entity to attach to the DbSet.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="entity"/> is null.</exception>
    protected virtual void AttachIfNot(TEntity entity)
    {
        ArgumentValidator.NotNull(entity);

        var entry = FindInChangeTracker(e => e == entity);
        if (entry == null || entry.State == EntityState.Detached)
            Table.Attach(entity);
    }

    /// <summary>
    /// Retrieves an entity from the ChangeTracker using its primary key or returns null if not found.
    /// </summary>
    /// <param name="id">The primary key of the entity to retrieve.</param>
    /// <returns>
    /// The entity tracked by the ChangeTracker if found, otherwise null.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="id"/> is null.</exception>
    private TEntity? GetFromChangeTrackerOrNull(TPrimaryKey id)
    {
        ArgumentValidator.NotNull(id!);

        var entry = FindInChangeTracker(entity =>
            EqualityComparer<TPrimaryKey>.Default.Equals(id, (entity as IEntity<TPrimaryKey>)!.Id)
        );
        return entry?.Entity;
    }

    /// <summary>
    /// Searches the ChangeTracker for an entity entry that matches the given predicate.
    /// </summary>
    /// <param name="predicate">The condition used to find the desired entity entry.</param>
    /// <returns>
    /// The <see cref="EntityEntry{TEntity}"/> that matches the predicate, or null if not found.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="predicate"/> is null.</exception>
    private EntityEntry<TEntity>? FindInChangeTracker(Func<TEntity, bool> predicate)
    {
        ArgumentValidator.NotNull(predicate);

        return Context
            .ChangeTracker.Entries<TEntity>()
            .FirstOrDefault(entry => predicate(entry.Entity));
    }
}
