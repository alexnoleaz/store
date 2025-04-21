using System.Linq.Expressions;
using Store.Shared.Dependency;
using Store.Shared.Entities;

namespace Store.Shared.Repositories;

public interface IRepository : IScopedDependency { }

public interface IRepository<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
{
    #region Select/Get/Query

    IQueryable<TEntity> GetAll();
    Task<IQueryable<TEntity>> GetAllAsync();
    IQueryable<TEntity> GetAllIncluding(
        params Expression<Func<TEntity, object>>[] propertySelectors
    );
    Task<IQueryable<TEntity>> GetAllIncludingAsync(
        params Expression<Func<TEntity, object>>[] propertySelectors
    );
    List<TEntity> GetAllList();
    Task<List<TEntity>> GetAllListAsync();
    List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);
    Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);
    TEntity? Get(TPrimaryKey id);
    Task<TEntity?> GetAsync(TPrimaryKey id);
    TEntity? Get(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);

    #endregion

    #region Insert

    TEntity Insert(TEntity entity);
    Task<TEntity> InsertAsync(TEntity entity);
    TPrimaryKey InsertAndGetId(TEntity entity);
    Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity);

    #endregion

    #region Update

    TEntity Update(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);

    #endregion

    #region Delete

    void Delete(TEntity entity);
    Task DeleteAsync(TEntity entity);
    void Delete(TPrimaryKey id);
    Task DeleteAsync(TPrimaryKey id);

    #endregion

    #region Aggregates

    int Count();
    Task<int> CountAsync();
    int Count(Expression<Func<TEntity, bool>> predicate);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
    long LongCount();
    Task<long> LongCountAsync();
    long LongCount(Expression<Func<TEntity, bool>> predicate);
    Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);

    #endregion

    int SaveChanges();
    Task<int> SaveChangesAsync();
}

public interface IRepository<TEntity> : IRepository<TEntity, int>
    where TEntity : class, IEntity { }