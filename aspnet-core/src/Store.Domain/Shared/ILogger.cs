namespace Store.Shared;

/// <summary>
/// Represents a logger interface for logging messages and exceptions with support for generic type context.
/// </summary>
/// <typeparam name="TCategory">The type whose name is used for the logger category.</typeparam>
public interface ILogger<TCategory>
{
    /// <summary>
    /// Logs a debug message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="args">The arguments to format the message.</param>
    void Debug(string? message, params object?[] args);

    /// <summary>
    /// Logs a debug message with an associated exception.
    /// </summary>
    /// <param name="exception">The exception to log.</param>
    /// <param name="message">The message to log.</param>
    /// <param name="args">The arguments to format the message.</param>
    void Debug(Exception? exception, string? message, params object?[] args);

    /// <summary>
    /// Logs an error message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="args">The arguments to format the message.</param>
    void Error(string? message, params object?[] args);

    /// <summary>
    /// Logs an error message with an associated exception.
    /// </summary>
    /// <param name="exception">The exception to log.</param>
    /// <param name="message">The message to log.</param>
    /// <param name="args">The arguments to format the message.</param>
    void Error(Exception? exception, string? message, params object?[] args);

    /// <summary>
    /// Logs an informational message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="args">The arguments to format the message.</param>
    void Information(string? message, params object?[] args);

    /// <summary>
    /// Logs an informational message with an associated exception.
    /// </summary>
    /// <param name="exception">The exception to log.</param>
    /// <param name="message">The message to log.</param>
    /// <param name="args">The arguments to format the message.</param>
    void Information(Exception? exception, string? message, params object?[] args);

    /// <summary>
    /// Logs a warning message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="args">The arguments to format the message.</param>
    void Warning(string? message, params object?[] args);

    /// <summary>
    /// Logs a warning message with an associated exception.
    /// </summary>
    /// <param name="exception">The exception to log.</param>
    /// <param name="message">The message to log.</param>
    /// <param name="args">The arguments to format the message.</param>
    void Warning(Exception? exception, string? message, params object?[] args);
}
