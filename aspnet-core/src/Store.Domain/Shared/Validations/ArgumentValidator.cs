namespace Store.Shared.Validations;

/// <summary>
/// Provides methods to validate arguments for null values.
/// </summary>
public static class ArgumentValidator
{
    /// <summary>
    /// Ensures the specified argument is not null.
    /// </summary>
    /// <param name="argument">The argument to validate.</param>
    /// <exception cref="ArgumentNullException">Thrown when the argument is null.</exception>
    public static void NotNull(object argument) =>
        ArgumentNullException.ThrowIfNull(argument, nameof(argument));

    /// <summary>
    /// Ensures the specified collection is not null and does not contain null elements.
    /// </summary>
    /// <param name="arguments">The collection of arguments to validate.</param>
    /// <exception cref="ArgumentNullException">Thrown when the collection is null or contains null elements.</exception>
    public static void NotNull(IEnumerable<object> arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments, nameof(arguments));

        if (arguments.Any(argument => argument is null))
        {
            throw new ArgumentNullException(
                nameof(arguments),
                "The collection cannot contain null elements."
            );
        }
    }
}
