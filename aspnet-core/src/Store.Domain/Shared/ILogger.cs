namespace Store.Shared;

/// <summary>
/// Defines a contract for logging messages and exceptions at various log levels.
/// </summary>
/// <typeparam name="TCategoryName">The category name for the logger, typically the type being logged.</typeparam>
public interface ILogger<TCategoryName>
{
    /// <summary>
    /// Logs a debug message with an optional exception.
    /// </summary>
    /// <param name="exception">The exception to log, or null if no exception.</param>
    /// <param name="message">The message to log.</param>
    /// <param name="args">Additional arguments for the message format.</param>
    void Debug(Exception? exception, string? message, params object?[] args);

    /// <summary>
    /// Logs a debug message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="args">Additional arguments for the message format.</param>
    void Debug(string? message, params object?[] args);

    /// <summary>
    /// Logs an error message with an optional exception.
    /// </summary>
    /// <param name="exception">The exception to log, or null if no exception.</param>
    /// <param name="message">The message to log.</param>
    /// <param name="args">Additional arguments for the message format.</param>
    void Error(Exception? exception, string? message, params object?[] args);

    /// <summary>
    /// Logs an error message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="args">Additional arguments for the message format.</param>
    void Error(string? message, params object?[] args);

    /// <summary>
    /// Logs an informational message with an optional exception.
    /// </summary>
    /// <param name="exception">The exception to log, or null if no exception.</param>
    /// <param name="message">The message to log.</param>
    /// <param name="args">Additional arguments for the message format.</param>
    void Information(Exception? exception, string? message, params object?[] args);

    /// <summary>
    /// Logs an informational message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="args">Additional arguments for the message format.</param>
    void Information(string? message, params object?[] args);

    /// <summary>
    /// Logs a warning message with an optional exception.
    /// </summary>
    /// <param name="exception">The exception to log, or null if no exception.</param>
    /// <param name="message">The message to log.</param>
    /// <param name="args">Additional arguments for the message format.</param>
    void Warning(Exception? exception, string? message, params object?[] args);

    /// <summary>
    /// Logs a warning message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="args">Additional arguments for the message format.</param>
    void Warning(string? message, params object?[] args);
}
