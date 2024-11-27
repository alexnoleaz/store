namespace Store.Shared;

/// <summary>
/// Provides methods for logging information, warnings, errors, and debug messages.
/// </summary>
public interface ILogger
{
    /// <summary>
    /// Logs an informational message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="args">Optional parameters to format the message.</param>
    void Information(string? message, params object?[] args);

    /// <summary>
    /// Logs an informational message with an exception.
    /// </summary>
    /// <param name="exception">The exception to include in the log.</param>
    /// <param name="message">The message to log.</param>
    /// <param name="args">Optional parameters to format the message.</param>
    void Information(Exception? exception, string? message, params object?[] args);

    /// <summary>
    /// Logs an error message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="args">Optional parameters to format the message.</param>
    void Error(string? message, params object?[] args);

    /// <summary>
    /// Logs an error message with an exception.
    /// </summary>
    /// <param name="exception">The exception to include in the log.</param>
    /// <param name="message">The message to log.</param>
    /// <param name="args">Optional parameters to format the message.</param>
    void Error(Exception? exception, string? message, params object?[] args);

    /// <summary>
    /// Logs a warning message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="args">Optional parameters to format the message.</param>
    void Warning(string? message, params object?[] args);

    /// <summary>
    /// Logs a warning message with an exception.
    /// </summary>
    /// <param name="exception">The exception to include in the log.</param>
    /// <param name="message">The message to log.</param>
    /// <param name="args">Optional parameters to format the message.</param>
    void Warning(Exception? exception, string? message, params object?[] args);

    /// <summary>
    /// Logs a debug message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="args">Optional parameters to format the message.</param>
    void Debug(string? message, params object?[] args);

    /// <summary>
    /// Logs a debug message with an exception.
    /// </summary>
    /// <param name="exception">The exception to include in the log.</param>
    /// <param name="message">The message to log.</param>
    /// <param name="args">Optional parameters to format the message.</param>
    void Debug(Exception? exception, string? message, params object?[] args);
}
