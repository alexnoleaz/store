using System.Reflection;

namespace Store.Shared.Reflection;

/// <summary>
/// Provides utility methods for working with types.
/// </summary>
public static class TypeHelper
{
    /// <summary>
    /// Retrieves the <see cref="Type"/> of the provided object.
    /// </summary>
    /// <param name="obj">The object to inspect.</param>
    /// <returns>The type of the object.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="obj"/> is null.</exception>
    public static Type Get(object obj)
    {
        ArgumentNullException.ThrowIfNull(obj, nameof(obj));
        return obj.GetType();
    }

    /// <summary>
    /// Retrieves the specified generic type.
    /// </summary>
    /// <typeparam name="T">The generic type.</typeparam>
    /// <returns>The specified generic type.</returns>
    public static Type Get<T>() => typeof(T);

    /// <summary>
    /// Determines if one type can be assigned from another.
    /// </summary>
    /// <param name="currentType">The target type.</param>
    /// <param name="sourceType">The source type.</param>
    /// <returns><c>true</c> if the target type is assignable from the source type; otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if either argument is null.</exception>
    public static bool IsAssignableFrom(Type currentType, Type sourceType)
    {
        ArgumentNullException.ThrowIfNull(currentType, nameof(currentType));
        ArgumentNullException.ThrowIfNull(sourceType, nameof(sourceType));
        return currentType.IsAssignableFrom(sourceType);
    }

    /// <summary>
    /// Determines if one type can be assigned to another.
    /// </summary>
    /// <param name="currentType">The source type.</param>
    /// <param name="targetType">The target type.</param>
    /// <returns><c>true</c> if the source type is assignable to the target type; otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if either argument is null.</exception>
    public static bool IsAssignableTo(Type currentType, Type targetType)
    {
        ArgumentNullException.ThrowIfNull(currentType, nameof(currentType));
        ArgumentNullException.ThrowIfNull(targetType, nameof(targetType));
        return currentType.IsAssignableTo(targetType);
    }

    /// <summary>
    /// Retrieves the properties of the specified type.
    /// </summary>
    /// <param name="type">The type to inspect.</param>
    /// <returns>A collection of properties defined in the type.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="type"/> is null.</exception>
    public static IEnumerable<PropertyInfo> GetProperties(Type type)
    {
        ArgumentNullException.ThrowIfNull(type, nameof(type));
        return type.GetProperties();
    }

    /// <summary>
    /// Retrieves the property with the specified name from the provided type.
    /// </summary>
    /// <param name="type">The type to inspect.</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <returns>The matching property, or <c>null</c> if not found.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="type"/> or <paramref name="propertyName"/> is null.</exception>
    public static PropertyInfo? GetProperty(Type type, string propertyName)
    {
        ArgumentNullException.ThrowIfNull(type, nameof(type));
        ArgumentNullException.ThrowIfNull(propertyName, nameof(propertyName));
        return type.GetProperty(propertyName);
    }
}
