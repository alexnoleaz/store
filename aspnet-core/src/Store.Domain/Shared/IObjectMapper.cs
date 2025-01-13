namespace Store.Shared;

/// <summary>
/// Provides methods for object mapping and projection.
/// </summary>
public interface IObjectMapper
{
    /// <summary>
    /// Maps an object to a destination type.
    /// </summary>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    /// <param name="source">The source object to map.</param>
    /// <returns>An instance of <typeparamref name="TDestination"/> mapped from the source object.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
    TDestination Map<TDestination>(object source);

    /// <summary>
    /// Maps a source object to an existing destination object.
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    /// <param name="source">The source object to map.</param>
    /// <param name="destination">The destination object to populate.</param>
    /// <returns>The populated <typeparamref name="TDestination"/> object.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> or <paramref name="destination"/> is null.</exception>
    TDestination Map<TSource, TDestination>(TSource source, TDestination destination);

    /// <summary>
    /// Projects a queryable source to a queryable destination type.
    /// </summary>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    /// <param name="source">The queryable source to project.</param>
    /// <returns>A queryable of <typeparamref name="TDestination"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
    IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source);
}
