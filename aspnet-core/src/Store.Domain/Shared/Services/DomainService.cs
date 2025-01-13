namespace Store.Shared.Services;

/// <summary>
/// Base class for domain services, extending <see cref="ServiceBase{TCategoryName}"/> with domain-specific functionality.
/// </summary>
/// <typeparam name="TCategoryName">The category name used for logging.</typeparam>
public abstract class DomainService<TCategoryName> : ServiceBase<TCategoryName>, IDomainService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainService{TCategoryName}"/> class.
    /// </summary>
    /// <param name="logger">The logger instance to use.</param>
    /// <param name="objectMapper">The object mapper instance to use.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="logger"/> or <paramref name="objectMapper"/> is null.</exception>
    protected DomainService(ILogger<TCategoryName> logger, IObjectMapper objectMapper)
        : base(logger, objectMapper) { }
}
