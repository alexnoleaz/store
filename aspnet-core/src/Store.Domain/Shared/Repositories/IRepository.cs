using System.Linq.Expressions;
using Store.Shared.Dependency;
using Store.Shared.Entities;

namespace Store.Shared.Repositories;

/// <summary>
/// A shortcut for <see cref="IRepository{TEntity,TPrimaryKey}"/> where the primary key type is <see cref="int"/>.
/// This interface simplifies the usage of repositories with the most common primary key type (<see cref="int"/>).
/// </summary>
/// <typeparam name="TEntity">
/// The type of the entity that the repository works with. The entity must implement <see cref="IEntity"/>.
/// </typeparam>
public interface IRepository<TEntity> : IRepository<TEntity, int>
    where TEntity : class, IEntity { }

/// <summary>
/// This interface must be implemented by all repositories to ensure the implementation of standard CRUD operations.
/// It provides methods for selecting, inserting, updating, and deleting entities.
/// </summary>
/// <typeparam name="TEntity">The main entity type that this repository operates on</typeparam>
/// <typeparam name="TPrimaryKey">The type of the primary key of the entity</typeparam>
public interface IRepository<TEntity, TPrimaryKey> : IRepository
    where TEntity : class, IEntity<TPrimaryKey>
{
    #region Select/Get/Query

    /// <summary>
    /// Used to get a IQueryable that is used to retrieve entities from entire table.
    /// </summary>
    /// <returns>IQueryable to be used to select entities from database</returns>
    IQueryable<TEntity> GetAll();

    /// <summary>
    /// Asynchronously gets all entities from the repository as an IQueryable.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation, with an IQueryable of entities</returns>
    Task<IQueryable<TEntity>> GetAllAsync();

    /// <summary>
    /// Gets all entities from the repository with additional included properties.
    /// </summary>
    /// <param name="propertySelectors">An array of expressions to include related properties</param>
    /// <returns>An IQueryable to retrieve entities with related properties</returns>
    IQueryable<TEntity> GetAllIncluding(
        params Expression<Func<TEntity, object>>[] propertySelectors
    );

    /// <summary>
    /// Asynchronously gets all entities from the repository with additional included properties.
    /// </summary>
    /// <param name="propertySelectors">An array of expressions to include related properties</param>
    /// <returns>A Task representing the asynchronous operation, with an IQueryable of entities</returns>
    Task<IQueryable<TEntity>> GetAllIncludingAsync(
        params Expression<Func<TEntity, object>>[] propertySelectors
    );

    /// <summary>
    /// Gets all entities from the repository as a list.
    /// </summary>
    /// <returns>A list of all entities</returns>
    List<TEntity> GetAllList();

    /// <summary>
    /// Asynchronously gets all entities from the repository as a list.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation, with a list of entities</returns>
    Task<List<TEntity>> GetAllListAsync();

    /// <summary>
    /// Gets all entities that match a specified condition.
    /// </summary>
    /// <param name="predicate">A condition to filter the entities</param>
    /// <returns>A list of entities that match the predicate</returns>
    List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Asynchronously gets all entities that match a specified condition.
    /// </summary>
    /// <param name="predicate">A condition to filter the entities</param>
    /// <returns>A Task representing the asynchronous operation, with a list of entities</returns>
    Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Gets an entity by its primary key.
    /// Returns the first entity that matches the given primary key.
    /// Throws an <see cref="EntityNotFoundException"/> if no entity is found with the provided primary key.
    /// </summary>
    /// <param name="id">The primary key of the entity</param>
    /// <returns>The entity with the given primary key</returns>
    /// <exception cref="EntityNotFoundException">Thrown when no entity is found with the provided primary key.</exception>
    TEntity Get(TPrimaryKey id);

    /// <summary>
    /// Asynchronously gets an entity by its primary key.
    /// Returns the first entity that matches the given primary key.
    /// Throws an <see cref="EntityNotFoundException"/> if no entity is found with the provided primary key.
    /// </summary>
    /// <param name="id">The primary key of the entity</param>
    /// <returns>A Task representing the asynchronous operation, with the entity</returns>
    /// <exception cref="EntityNotFoundException">Thrown when no entity is found with the provided primary key.</exception>
    Task<TEntity> GetAsync(TPrimaryKey id);

    /// <summary>
    /// Gets an entity that matches the given predicate.
    /// Returns the first entity that matches the condition defined in the predicate,
    /// or <c>null</c> if no entity matches the condition.
    /// </summary>
    /// <param name="predicate">A condition to filter the entity.</param>
    /// <returns>The entity matching the predicate, or <c>null</c> if no match is found.</returns>
    TEntity? Get(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Asynchronously gets an entity that matches the given predicate.
    /// Returns the first entity that matches the condition defined in the predicate,
    /// or <c>null</c> if no entity matches the condition.
    /// </summary>
    /// <param name="predicate">A condition to filter the entity.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, with the entity matching the predicate, or <c>null</c> if no match is found.</returns>
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);
    #endregion

    #region Insert

    /// <summary>
    /// Inserts a new entity into the repository.
    /// </summary>
    /// <param name="entity">The entity to insert</param>
    /// <returns>The inserted entity</returns>
    TEntity Insert(TEntity entity);

    /// <summary>
    /// Asynchronously inserts a new entity into the repository.
    /// </summary>
    /// <param name="entity">The entity to insert</param>
    /// <returns>A Task representing the asynchronous operation, with the inserted entity</returns>
    Task<TEntity> InsertAsync(TEntity entity);

    /// <summary>
    /// Inserts a new entity and returns its primary key.
    /// </summary>
    /// <param name="entity">The entity to insert</param>
    /// <returns>The primary key of the inserted entity</returns>
    TPrimaryKey InsertAndGetId(TEntity entity);

    /// <summary>
    /// Asynchronously inserts a new entity and returns its primary key.
    /// </summary>
    /// <param name="entity">The entity to insert</param>
    /// <returns>A Task representing the asynchronous operation, with the primary key of the inserted entity</returns>
    Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity);

    #endregion

    #region Update

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to update</param>
    /// <returns>The updated entity</returns>
    TEntity Update(TEntity entity);

    /// <summary>
    /// Asynchronously updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to update</param>
    /// <returns>A Task representing the asynchronous operation, with the updated entity</returns>
    Task<TEntity> UpdateAsync(TEntity entity);

    #endregion

    #region Delete

    /// <summary>
    /// Deletes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to delete</param>
    void Delete(TEntity entity);

    /// <summary>
    /// Asynchronously deletes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to delete</param>
    Task DeleteAsync(TEntity entity);

    /// <summary>
    /// Deletes an entity by its primary key.
    /// </summary>
    /// <param name="id">The primary key of the entity to delete</param>
    void Delete(TPrimaryKey id);

    /// <summary>
    /// Asynchronously deletes an entity by its primary key.
    /// </summary>
    /// <param name="id">The primary key of the entity to delete</param>
    Task DeleteAsync(TPrimaryKey id);

    #endregion

    #region Aggregates

    /// <summary>
    /// Retrieves the count of all entities in this repository.
    /// </summary>
    /// <returns>
    /// The total number of entities in the repository.
    /// </returns>
    int Count();

    /// <summary>
    /// Retrieves the count of all entities in this repository asynchronously.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation, containing the total number of entities in the repository.
    /// </returns>
    Task<int> CountAsync();

    /// <summary>
    /// Retrieves the count of all entities that match the given <paramref name="predicate"/> in this repository.
    /// </summary>
    /// <param name="predicate">
    /// A condition to filter entities for counting.
    /// </param>
    /// <returns>
    /// The total number of entities matching the given condition.
    /// </returns>
    int Count(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves the count of all entities that match the given <paramref name="predicate"/> in this repository asynchronously.
    /// </summary>
    /// <param name="predicate">
    /// A condition to filter entities for counting.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation, containing the total number of entities matching the condition.
    /// </returns>
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves the long count of all entities in this repository.
    /// This is useful when the expected return value exceeds <see cref="int.MaxValue"/>.
    /// </summary>
    /// <returns>
    /// The total number of entities in the repository as a long.
    /// </returns>
    long LongCount();

    /// <summary>
    /// Retrieves the long count of all entities in this repository asynchronously.
    /// This is useful when the expected return value exceeds <see cref="int.MaxValue"/>.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation, containing the total number of entities in the repository as a long.
    /// </returns>
    Task<long> LongCountAsync();

    /// <summary>
    /// Retrieves the long count of all entities that match the given <paramref name="predicate"/> in this repository.
    /// This is useful when the expected return value exceeds <see cref="int.MaxValue"/>.
    /// </summary>
    /// <param name="predicate">
    /// A condition to filter entities for counting.
    /// </param>
    /// <returns>
    /// The total number of entities matching the given condition as a long.
    /// </returns>
    long LongCount(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves the long count of all entities that match the given <paramref name="predicate"/> in this repository asynchronously.
    /// This is useful when the expected return value exceeds <see cref="int.MaxValue"/>.
    /// </summary>
    /// <param name="predicate">
    /// A condition to filter entities for counting.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation, containing the total number of entities matching the condition as a long.
    /// </returns>
    Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);
    #endregion
}

/// <summary>
/// This interface serves as a marker interface to identify repository types by convention.
/// It is recommended to implement the generic version of this interface instead of using this one directly.
/// </summary>
public interface IRepository : IScopedDependency { }
