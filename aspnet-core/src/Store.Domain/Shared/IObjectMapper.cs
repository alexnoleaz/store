namespace Store.Shared;

/// <summary>
/// Provides methods for object mapping and projection.
/// </summary>
public interface IObjectMapper
{
    /// <summary>
    /// Maps an object to a new instance of the specified destination type.
    /// </summary>
    /// <typeparam name="TDestination">The type to which the object should be mapped.</typeparam>
    /// <param name="source">The source object to map from.</param>
    /// <returns>A new instance of <typeparamref name="TDestination"/>.</returns>
    TDestination Map<TDestination>(object source);

    /// <summary>
    /// Maps a source object to an existing destination object.
    /// </summary>
    /// <typeparam name="TSource">The type of the source object.</typeparam>
    /// <typeparam name="TDestination">The type of the destination object.</typeparam>
    /// <param name="source">The source object to map from.</param>
    /// <param name="destination">The destination object to map to.</param>
    /// <returns>The updated <paramref name="destination"/> object.</returns>
    TDestination Map<TSource, TDestination>(TSource source, TDestination destination);

    /// <summary>
    /// Projects an <see cref="IQueryable"/> source to an <see cref="IQueryable"/> of the specified destination type.
    /// </summary>
    /// <typeparam name="TDestination">The type to which the queryable should be projected.</typeparam>
    /// <param name="source">The source queryable to project from.</param>
    /// <returns>An <see cref="IQueryable{T}"/> of <typeparamref name="TDestination"/>.</returns>
    IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source);
}