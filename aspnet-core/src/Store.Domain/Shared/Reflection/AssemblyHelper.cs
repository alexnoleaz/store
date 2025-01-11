using System.Reflection;
using Store.Shared.Validations;

namespace Store.Shared.Reflection;

/// <summary>
/// Provides utility methods for working with assemblies.
/// </summary>
public static class AssemblyHelper
{
    /// <summary>
    /// Gets the distinct assemblies of the specified objects.
    /// </summary>
    /// <param name="objects">The objects whose assemblies are to be determined.</param>
    /// <returns>A collection of distinct assemblies.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the objects collection is null or contains null elements.</exception>
    public static IEnumerable<Assembly> GetAssemblies(IEnumerable<object> objects)
    {
        ArgumentValidator.NotNull(objects);
        return objects.Select(o => o.GetType().Assembly).Distinct();
    }

    /// <summary>
    /// Gets the distinct assemblies of the specified objects.
    /// </summary>
    /// <param name="objects">The objects whose assemblies are to be determined.</param>
    /// <returns>A collection of distinct assemblies.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the objects array is null or contains null elements.</exception>
    public static IEnumerable<Assembly> GetAssemblies(params object[] objects) =>
        GetAssemblies((IEnumerable<object>)objects);

    /// <summary>
    /// Gets the assembly of the specified object.
    /// </summary>
    /// <param name="obj">The object whose assembly is to be determined.</param>
    /// <returns>The <see cref="Assembly"/> of the object.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the object is null.</exception>
    public static Assembly Get(object obj)
    {
        ArgumentValidator.NotNull(obj);
        return obj.GetType().Assembly;
    }

    /// <summary>
    /// Gets the assembly of the specified generic type.
    /// </summary>
    /// <typeparam name="T">The type parameter.</typeparam>
    /// <returns>The <see cref="Assembly"/> of the generic type.</returns>
    public static Assembly Get<T>() => typeof(T).Assembly;
}
