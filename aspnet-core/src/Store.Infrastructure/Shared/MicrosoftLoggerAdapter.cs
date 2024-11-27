using Microsoft.Extensions.Logging;
using Store.Shared.Dependency;

namespace Store.Shared;

/// <summary>
/// Provides a concrete implementation of <see cref="ILogger"/>
/// by wrapping <see cref="Microsoft.Extensions.Logging.ILogger"/>.
/// </summary>
public class MicrosoftLoggerAdapter : ILogger, ISingletonDependency
{
    private readonly Microsoft.Extensions.Logging.ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="RegularLogger"/> class.
    /// </summary>
    /// <param name="logger">The underlying Microsoft.Extensions.Logging logger instance.</param>
    public MicrosoftLoggerAdapter(Microsoft.Extensions.Logging.ILogger logger) => _logger = logger;

    /// <inheritdoc />
    public void Information(string? message, params object?[] args) =>
        _logger.LogInformation(message, args);

    /// <inheritdoc />
    public void Information(Exception? exception, string? message, params object?[] args) =>
        _logger.LogInformation(exception, message, args);

    /// <inheritdoc />
    public void Error(string? message, params object?[] args) => _logger.LogError(message, args);

    /// <inheritdoc />
    public void Error(Exception? exception, string? message, params object?[] args) =>
        _logger.LogError(exception, message, args);

    /// <inheritdoc />
    public void Warning(string? message, params object?[] args) =>
        _logger.LogWarning(message, args);

    /// <inheritdoc />
    public void Warning(Exception? exception, string? message, params object?[] args) =>
        _logger.LogWarning(exception, message, args);

    /// <inheritdoc />
    public void Debug(string? message, params object?[] args) => _logger.LogDebug(message, args);

    /// <inheritdoc />
    public void Debug(Exception? exception, string? message, params object?[] args) =>
        _logger.LogDebug(exception, message, args);
}
