namespace Store.Shared;

/// <summary>
/// Defines methods for mapping objects between different types and projecting queries to a target type.
/// </summary>
public interface IObjectMapper
{
    /// <summary>
    /// Maps an object to a new instance of the specified destination type.
    /// </summary>
    /// <typeparam name="TDestination">The type of the destination object.</typeparam>
    /// <param name="source">The source object to map.</param>
    /// <returns>A new instance of <typeparamref name="TDestination"/>.</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="source"/> is <c>null</c>.
    /// </exception>
    TDestination Map<TDestination>(object source);

    /// <summary>
    /// Maps the source object to an existing instance of the specified destination type.
    /// </summary>
    /// <typeparam name="TSource">The type of the source object.</typeparam>
    /// <typeparam name="TDestination">The type of the destination object.</typeparam>
    /// <param name="source">The source object to map.</param>
    /// <param name="destination">The existing destination object to populate.</param>
    /// <returns>The populated <typeparamref name="TDestination"/> instance.</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="source"/> or <paramref name="destination"/> is <c>null</c>.
    /// </exception>
    TDestination Map<TSource, TDestination>(TSource source, TDestination destination);

    /// <summary>
    /// Projects a queryable source to the specified destination type.
    /// </summary>
    /// <typeparam name="TDestination">The type of the destination objects in the query.</typeparam>
    /// <param name="source">The queryable source to project.</param>
    /// <returns>A queryable collection of <typeparamref name="TDestination"/>.</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="source"/> is <c>null</c>.
    /// </exception>
    IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source);
}
