namespace Store.Shared.Services;

/// <summary>
/// Provides common functionality for services, including logging and object mapping.
/// </summary>
public abstract class ServiceBase
{
    /// <summary>
    /// Gets the logger instance used for logging service activities.
    /// </summary>
    protected ILogger Logger { get; }

    /// <summary>
    /// Gets the object mapper instance used for mapping between objects.
    /// </summary>
    protected IObjectMapper ObjectMapper { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceBase"/> class.
    /// </summary>
    /// <param name="logger">The logger instance to be used for logging.</param>
    /// <param name="objectMapper">The object mapper instance to be used for object mapping.</param>
    protected ServiceBase(ILogger logger, IObjectMapper objectMapper)
    {
        Logger = logger;
        ObjectMapper = objectMapper;
    }
}
