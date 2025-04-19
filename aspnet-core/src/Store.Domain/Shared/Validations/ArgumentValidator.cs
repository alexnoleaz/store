namespace Store.Shared.Validations;

public static class ArgumentValidator
{
    public static void NotNull(object argument) => ArgumentNullException.ThrowIfNull(argument);

    public static void NotNull(IEnumerable<object> arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);

        if (arguments.Any(argument => argument is null))
            throw new ArgumentNullException(
                nameof(arguments),
                "The collection cannot contain null elements."
            );
    }
}
