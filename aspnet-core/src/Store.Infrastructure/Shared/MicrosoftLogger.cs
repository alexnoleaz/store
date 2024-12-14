using Microsoft.Extensions.Logging;

namespace Store.Shared;

/// <summary>
/// Provides a concrete implementation of <see cref="ILogger{T}"/> using <see cref="Microsoft.Extensions.Logging"/>.
/// </summary>
/// <typeparam name="TCategory">The type whose name is used for the logger category.</typeparam>
public class MicrosoftLogger<TCategory> : ILogger<TCategory>
{
    private readonly Microsoft.Extensions.Logging.ILogger<TCategory> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="MicrosoftLogger{T}"/> class.
    /// </summary>
    /// <param name="logger">The underlying Microsoft.Extensions.Logging logger.</param>
    public MicrosoftLogger(Microsoft.Extensions.Logging.ILogger<TCategory> logger)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        _logger = logger;
    }

    /// <inheritdoc />
    public void Debug(string? message, params object?[] args) => _logger.LogDebug(message, args);

    /// <inheritdoc />
    public void Debug(Exception? exception, string? message, params object?[] args) =>
        _logger.LogDebug(exception, message, args);

    /// <inheritdoc />
    public void Error(string? message, params object?[] args) => _logger.LogError(message, args);

    /// <inheritdoc />
    public void Error(Exception? exception, string? message, params object?[] args) =>
        _logger.LogError(exception, message, args);

    /// <inheritdoc />
    public void Information(string? message, params object?[] args) =>
        _logger.LogInformation(message, args);

    /// <inheritdoc />
    public void Information(Exception? exception, string? message, params object?[] args) =>
        _logger.LogInformation(exception, message, args);

    /// <inheritdoc />
    public void Warning(string? message, params object?[] args) =>
        _logger.LogWarning(message, args);

    /// <inheritdoc />
    public void Warning(Exception? exception, string? message, params object?[] args) =>
        _logger.LogWarning(exception, message, args);
}
