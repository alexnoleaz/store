using System.Reflection;

namespace Store.Shared.Reflection;

/// <summary>
/// Provides utility methods for working with types.
/// </summary>
public static class TypeHelper
{
    /// <summary>
    /// Retrieves the runtime type of the specified object.
    /// </summary>
    /// <param name="obj">The object whose runtime type is to be retrieved.</param>
    /// <returns>The runtime type of the specified object.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="obj"/> is null.</exception>
    public static Type Get(object obj)
    {
        ArgumentNullException.ThrowIfNull(obj, nameof(obj));
        return obj.GetType();
    }

    /// <summary>
    /// Retrieves the type of the specified generic type.
    /// </summary>
    /// <typeparam name="T">The generic type whose type is to be retrieved.</typeparam>
    /// <returns>The type of the specified generic type.</returns>
    public static Type Get<T>() => typeof(T);

    /// <summary>
    /// Determines whether the current type is assignable from the specified source type.
    /// </summary>
    /// <param name="currentType">The current type to check.</param>
    /// <param name="sourceType">The source type to check against.</param>
    /// <returns><see langword="true"/> if the current type is assignable from the source type; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="currentType"/> or <paramref name="sourceType"/> is null.</exception>
    public static bool IsAssignableFrom(Type currentType, Type sourceType)
    {
        ArgumentNullException.ThrowIfNull(currentType, nameof(currentType));
        ArgumentNullException.ThrowIfNull(sourceType, nameof(sourceType));
        return currentType.IsAssignableFrom(sourceType);
    }

    /// <summary>
    /// Determines whether the current type is assignable to the specified target type.
    /// </summary>
    /// <param name="currentType">The current type to check.</param>
    /// <param name="targetType">The target type to check against.</param>
    /// <returns><see langword="true"/> if the current type is assignable to the target type; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="currentType"/> or <paramref name="targetType"/> is null.</exception>
    public static bool IsAssignableTo(Type currentType, Type targetType)
    {
        ArgumentNullException.ThrowIfNull(currentType, nameof(currentType));
        ArgumentNullException.ThrowIfNull(targetType, nameof(targetType));
        return currentType.IsAssignableTo(targetType);
    }

    /// <summary>
    /// Retrieves the properties of the specified type.
    /// </summary>
    /// <param name="type">The type whose properties are to be retrieved.</param>
    /// <returns>A collection of properties of the specified type.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="type"/> is null.</exception>
    public static IEnumerable<PropertyInfo> GetProperties(Type type)
    {
        ArgumentNullException.ThrowIfNull(type, nameof(type));
        return type.GetProperties();
    }

    /// <summary>
    /// Retrieves the property with the specified name from the specified type.
    /// </summary>
    /// <param name="type">The type whose property is to be retrieved.</param>
    /// <param name="propertyName">The name of the property to retrieve.</param>
    /// <returns>The property with the specified name, or <see langword="null"/> if no such property exists.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="type"/> or <paramref name="propertyName"/> is null.</exception>
    public static PropertyInfo? GetProperty(Type type, string propertyName)
    {
        ArgumentNullException.ThrowIfNull(type, nameof(type));
        ArgumentNullException.ThrowIfNull(propertyName, nameof(propertyName));
        return type.GetProperty(propertyName);
    }
}
