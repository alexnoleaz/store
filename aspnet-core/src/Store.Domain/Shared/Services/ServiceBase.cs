using Store.Shared.Validations;

namespace Store.Shared.Services;

/// <summary>
/// Base class for services that provides logging and object mapping capabilities.
/// </summary>
/// <typeparam name="TCategoryName">The category name used for logging.</typeparam>
public abstract class ServiceBase<TCategoryName>
{
    /// <summary>
    /// Gets the logger associated with the service.
    /// </summary>
    protected ILogger<TCategoryName> Logger { get; }

    /// <summary>
    /// Gets the object mapper for mapping objects.
    /// </summary>
    protected IObjectMapper ObjectMapper { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceBase{TCategoryName}"/> class.
    /// </summary>
    /// <param name="logger">The logger instance to use.</param>
    /// <param name="objectMapper">The object mapper instance to use.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="logger"/> or <paramref name="objectMapper"/> is null.</exception>
    protected ServiceBase(ILogger<TCategoryName> logger, IObjectMapper objectMapper)
    {
        ArgumentValidator.NotNull(logger);
        ArgumentValidator.NotNull(objectMapper);

        Logger = logger;
        ObjectMapper = objectMapper;
    }
}