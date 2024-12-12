using System.Reflection;

namespace Store.Shared.Reflection;

/// <summary>
/// Provides utility methods for working with assemblies.
/// </summary>
public static class AssemblyHelper
{
    /// <summary>
    /// Retrieves the unique assemblies associated with the provided types.
    /// </summary>
    /// <param name="types">A collection of <see cref="Type"/> instances.</param>
    /// <returns>A collection of unique assemblies.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="types"/> is null or contains null elements.</exception>
    public static IEnumerable<Assembly> GetAssemblies(IEnumerable<Type> types)
    {
        ArgumentNullException.ThrowIfNull(types, nameof(types));
        if (types.Any(t => t is null))
            throw new ArgumentNullException(
                nameof(types),
                "The collection cannot contain null elements."
            );

        return types.Select(t => t.Assembly).Distinct();
    }

    /// <summary>
    /// Retrieves the unique assemblies associated with the provided types.
    /// </summary>
    /// <param name="types">An array of <see cref="Type"/> instances.</param>
    /// <returns>A collection of unique assemblies.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="types"/> is null or contains null elements.</exception>
    public static IEnumerable<Assembly> GetAssemblies(params Type[] types) =>
        GetAssemblies((IEnumerable<Type>)types);

    /// <summary>
    /// Retrieves the unique assemblies associated with the types of the provided objects.
    /// </summary>
    /// <param name="objects">A collection of objects.</param>
    /// <returns>A collection of unique assemblies.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="objects"/> is null or contains null elements.</exception>
    public static IEnumerable<Assembly> GetAssemblies(IEnumerable<object> objects)
    {
        ArgumentNullException.ThrowIfNull(objects, nameof(objects));
        if (objects.Any(o => o is null))
            throw new ArgumentNullException(
                nameof(objects),
                "The collection cannot contain null elements."
            );

        return objects.Select(o => o.GetType().Assembly).Distinct();
    }

    /// <summary>
    /// Retrieves the unique assemblies associated with the types of the provided objects.
    /// </summary>
    /// <param name="objects">An array of objects.</param>
    /// <returns>A collection of unique assemblies.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="objects"/> is null or contains null elements.</exception>
    public static IEnumerable<Assembly> GetAssemblies(params object[] objects) =>
        GetAssemblies((IEnumerable<object>)objects);

    /// <summary>
    /// Retrieves the assembly associated with the provided type.
    /// </summary>
    /// <param name="type">The <see cref="Type"/> instance.</param>
    /// <returns>The assembly of the type.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="type"/> is null.</exception>
    public static Assembly Get(Type type)
    {
        ArgumentNullException.ThrowIfNull(type, nameof(type));
        return type.Assembly;
    }

    /// <summary>
    /// Retrieves the assembly associated with the specified generic type.
    /// </summary>
    /// <typeparam name="T">The generic type.</typeparam>
    /// <returns>The assembly of the type.</returns>
    public static Assembly Get<T>() => typeof(T).Assembly;
}
