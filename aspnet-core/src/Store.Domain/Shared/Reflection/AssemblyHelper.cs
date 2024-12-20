using System.Reflection;

namespace Store.Shared.Reflection;

/// <summary>
/// Provides utility methods for working with assemblies.
/// </summary>
public static class AssemblyHelper
{
    /// <summary>
    /// Retrieves the distinct assemblies associated with the specified types.
    /// </summary>
    /// <param name="types">The collection of types whose assemblies are to be retrieved.</param>
    /// <returns>A collection of distinct assemblies associated with the specified types.</returns>
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
    /// Retrieves the distinct assemblies associated with the specified types.
    /// </summary>
    /// <param name="types">The array of types whose assemblies are to be retrieved.</param>
    /// <returns>A collection of distinct assemblies associated with the specified types.</returns>
    public static IEnumerable<Assembly> GetAssemblies(params Type[] types) =>
        GetAssemblies((IEnumerable<Type>)types);

    /// <summary>
    /// Retrieves the distinct assemblies associated with the runtime types of the specified objects.
    /// </summary>
    /// <param name="objects">The collection of objects whose runtime types' assemblies are to be retrieved.</param>
    /// <returns>A collection of distinct assemblies associated with the runtime types of the specified objects.</returns>
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
    /// Retrieves the distinct assemblies associated with the runtime types of the specified objects.
    /// </summary>
    /// <param name="objects">The array of objects whose runtime types' assemblies are to be retrieved.</param>
    /// <returns>A collection of distinct assemblies associated with the runtime types of the specified objects.</returns>
    public static IEnumerable<Assembly> GetAssemblies(params object[] objects) =>
        GetAssemblies((IEnumerable<object>)objects);

    /// <summary>
    /// Retrieves the assembly associated with the specified type.
    /// </summary>
    /// <param name="type">The type whose assembly is to be retrieved.</param>
    /// <returns>The assembly associated with the specified type.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="type"/> is null.</exception>
    public static Assembly Get(Type type)
    {
        ArgumentNullException.ThrowIfNull(type, nameof(type));
        return type.Assembly;
    }

    /// <summary>
    /// Retrieves the assembly associated with the specified generic type.
    /// </summary>
    /// <typeparam name="T">The generic type whose assembly is to be retrieved.</typeparam>
    /// <returns>The assembly associated with the specified generic type.</returns>
    public static Assembly Get<T>() => typeof(T).Assembly;
}
