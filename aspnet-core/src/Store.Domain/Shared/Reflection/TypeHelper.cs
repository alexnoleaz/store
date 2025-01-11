using Store.Shared.Validations;

namespace Store.Shared.Reflection;

/// <summary>
/// Provides utility methods for type operations.
/// </summary>
public static class TypeHelper
{
    /// <summary>
    /// Gets the runtime type of the specified object.
    /// </summary>
    /// <param name="obj">The object whose type is to be determined.</param>
    /// <returns>The <see cref="Type"/> of the object.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the object is null.</exception>
    public static Type Get(object obj)
    {
        ArgumentValidator.NotNull(obj);
        return obj.GetType();
    }

    /// <summary>
    /// Gets the type of the specified generic parameter.
    /// </summary>
    /// <typeparam name="T">The type parameter.</typeparam>
    /// <returns>The <see cref="Type"/> of the generic parameter.</returns>
    public static Type Get<T>() => typeof(T);
}
