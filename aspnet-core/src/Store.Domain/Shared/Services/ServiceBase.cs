namespace Store.Shared.Services;

/// <summary>
/// Provides a base class for services with built-in logging and object mapping capabilities.
/// </summary>
/// <typeparam name="TCategory">
/// The type used to define the category name for the logger. Typically, this is the type of the derived service class.
/// </typeparam>
public abstract class ServiceBase<TCategory>
{
    /// <summary>
    /// Gets the logger instance for the service, which uses <typeparamref name="TCategory"/> as the category name.
    /// </summary>
    protected ILogger<TCategory> Logger { get; }

    /// <summary>
    /// Gets the object mapper instance for the service.
    /// </summary>
    protected IObjectMapper ObjectMapper { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceBase{T}"/> class.
    /// </summary>
    /// <param name="logger">The logger instance to use for logging operations, categorized by <typeparamref name="TCategory"/>.</param>
    /// <param name="objectMapper">The object mapper instance to use for mapping objects.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="logger"/> or <paramref name="objectMapper"/> is <c>null</c>.
    /// </exception>
    protected ServiceBase(ILogger<TCategory> logger, IObjectMapper objectMapper)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(objectMapper, nameof(objectMapper));

        Logger = logger;
        ObjectMapper = objectMapper;
    }
}