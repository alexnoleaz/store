using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Store.Shared.Entities;

namespace Store.Shared.Repositories.EntityFrameworkCore;

/// <inheritdoc/>
public class EFCoreRepositoryBase<TEntity>
    : EFCoreRepositoryBase<TEntity, int>,
        IRepository<TEntity>
    where TEntity : class, IEntity<int>
{
    public EFCoreRepositoryBase(StoreDbContext context)
        : base(context) { }
}

/// <summary>
/// Implements IRepository for EntityFrameworkCore.
/// </summary>
/// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
/// <typeparam name="TPrimaryKey">Primary key of the entity</typeparam>
public class EFCoreRepositoryBase<TEntity, TPrimaryKey> : RepositoryBase<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="context">DbContext for this repository</param>
    public EFCoreRepositoryBase(StoreDbContext context)
    {
        System.Console.WriteLine(typeof(StoreDbContext));
        Context = context;
    }

    /// <summary>
    /// Provides the DbContext for this repository.
    /// </summary>
    protected StoreDbContext Context { get; }

    /// <summary>
    /// Provides DbSet for the entity.
    /// </summary>
    protected DbSet<TEntity> Table => Context.Set<TEntity>();

    /// <summary>
    /// Provides access to the <see cref="DatabaseFacade"/> instance for performing database-related operations,
    /// including execution of raw SQL commands and management of transactions.
    /// </summary>
    protected DatabaseFacade Database => Context.Database;

    /// <summary>
    /// Provides the active database connection for the current context.
    /// If the connection is not already open, it opens it before returning.
    /// </summary>
    protected DbConnection Connection
    {
        get
        {
            var connection = Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
                connection.Open();

            return connection;
        }
    }

    /// <inheritdoc/>
    public override IQueryable<TEntity> GetAll() => Table.AsQueryable();

    /// <inheritdoc/>
    public override Task<IQueryable<TEntity>> GetAllAsync() => Task.FromResult(Table.AsQueryable());

    /// <inheritdoc/>
    public override IQueryable<TEntity> GetAllIncluding(
        params Expression<Func<TEntity, object>>[] propertySelectors
    )
    {
        var query = GetAll();
        if (propertySelectors is { Length: > 0 })
            foreach (var selector in propertySelectors)
                query = query.Include(selector);

        return query;
    }

    /// <inheritdoc/>
    public override async Task<IQueryable<TEntity>> GetAllIncludingAsync(
        params Expression<Func<TEntity, object>>[] propertySelectors
    )
    {
        var query = await GetAllAsync();
        if (propertySelectors is { Length: > 0 })
            foreach (var selector in propertySelectors)
                query = query.Include(selector);

        return query;
    }

    /// <inheritdoc/>
    public override async Task<List<TEntity>> GetAllListAsync() => await Table.ToListAsync();

    /// <inheritdoc/>
    public override async Task<List<TEntity>> GetAllListAsync(
        Expression<Func<TEntity, bool>> predicate
    ) => await Table.Where(predicate).ToListAsync();

    /// <inheritdoc/>
    public override async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate) =>
        await Table.SingleAsync(predicate);

    /// <inheritdoc/>
    public override async Task<TEntity?> FirstOrDefaultAsync(TPrimaryKey id) =>
        await Table.FirstOrDefaultAsync(CreateEqualityExpressionForId(id));

    /// <inheritdoc/>
    public override async Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate
    ) => await Table.FirstOrDefaultAsync(predicate);

    /// <inheritdoc/>
    public override TEntity Insert(TEntity entity)
    {
        var result = Table.Add(entity);
        Context.SaveChanges();
        return result.Entity;
    }

    /// <inheritdoc/>
    public override async Task<TEntity> InsertAsync(TEntity entity)
    {
        var result = await Table.AddAsync(entity);
        await Context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc/>
    public override TEntity Update(TEntity entity)
    {
        AttachIfNotTracked(entity);
        Context.Entry(entity).State = EntityState.Modified;
        Context.SaveChanges();
        return entity;
    }

    /// <inheritdoc/>
    public override async Task<TEntity> UpdateAsync(TEntity entity)
    {
        AttachIfNotTracked(entity);
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc/>
    public override void Delete(TEntity entity)
    {
        AttachIfNotTracked(entity);
        Table.Remove(entity);
        Context.SaveChanges();
    }

    /// <inheritdoc/>
    public override async Task DeleteAsync(TEntity entity)
    {
        AttachIfNotTracked(entity);
        Table.Remove(entity);
        await Context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public override void Delete(TPrimaryKey id)
    {
        var entity = GetFromChangeTrackerOrNull(id) ?? FirstOrDefault(id);
        if (entity is not null)
            Delete(entity);
    }

    /// <inheritdoc/>
    public override async Task DeleteAsync(TPrimaryKey id)
    {
        var entity = GetFromChangeTrackerOrNull(id) ?? await FirstOrDefaultAsync(id);
        if (entity is not null)
            Delete(entity);
    }

    /// <inheritdoc/>

    public override async Task<int> CountAsync() => await Table.CountAsync();

    /// <inheritdoc/>
    public override async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate) =>
        await Table.CountAsync(predicate);

    /// <inheritdoc/>
    public override async Task<long> LongCountAsync() => await Table.LongCountAsync();

    /// <inheritdoc/>
    public override async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate) =>
        await Table.LongCountAsync(predicate);

    /// <summary>
    /// Attaches the entity to the context if not already tracked.
    /// </summary>
    protected void AttachIfNotTracked(TEntity entity)
    {
        if (Context.Entry(entity).State == EntityState.Detached)
            Table.Attach(entity);
    }

    private TEntity? GetFromChangeTrackerOrNull(TPrimaryKey id)
    {
        return Context
            .ChangeTracker.Entries<TEntity>()
            .FirstOrDefault(entry =>
                EqualityComparer<TPrimaryKey>.Default.Equals(entry.Entity.Id, id)
            )
            ?.Entity;
    }
}
