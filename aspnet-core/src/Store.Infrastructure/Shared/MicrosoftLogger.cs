using Microsoft.Extensions.Logging;
using Store.Shared.Validations;
using MsLogging = Microsoft.Extensions.Logging;

namespace Store.Shared;

/// <summary>
/// An implementation of <see cref="ILogger{TCategoryName}"/> using <see cref="Microsoft.Extensions.Logging.ILogger{TCategoryName}"/>.
/// </summary>
/// <typeparam name="TCategoryName">The category name for the logger, typically the type being logged.</typeparam>
public class MicrosoftLogger<TCategoryName> : ILogger<TCategoryName>
{
    private readonly MsLogging.ILogger<TCategoryName> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="MicrosoftLogger{TCategoryName}"/> class.
    /// </summary>
    /// <param name="logger">The underlying Microsoft.Extensions.Logging logger.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="logger"/> is null.</exception>
    public MicrosoftLogger(MsLogging.ILogger<TCategoryName> logger)
    {
        ArgumentValidator.NotNull(logger);
        _logger = logger;
    }

    /// <inheritdoc />
    public void Debug(Exception? exception, string? message, params object?[] args) =>
        _logger.LogDebug(exception, message, args);

    /// <inheritdoc />
    public void Debug(string? message, params object?[] args) => _logger.LogDebug(message, args);

    /// <inheritdoc />
    public void Error(Exception? exception, string? message, params object?[] args) =>
        _logger.LogError(exception, message, args);

    /// <inheritdoc />
    public void Error(string? message, params object?[] args) => _logger.LogError(message, args);

    /// <inheritdoc />
    public void Information(Exception? exception, string? message, params object?[] args) =>
        _logger.LogInformation(exception, message, args);

    /// <inheritdoc />
    public void Information(string? message, params object?[] args) =>
        _logger.LogInformation(message, args);

    /// <inheritdoc />
    public void Warning(Exception? exception, string? message, params object?[] args) =>
        _logger.LogWarning(exception, message, args);

    /// <inheritdoc />
    public void Warning(string? message, params object?[] args) =>
        _logger.LogWarning(message, args);
}
