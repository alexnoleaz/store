namespace Store.Shared.Services;

/// <summary>
/// Abstract base class for domain services, providing common functionality such as logging and object mapping.
/// Inherits from <see cref="ServiceBase"/> and implements <see cref="IDomainService"/>.
/// </summary>
public abstract class DomainService : ServiceBase, IDomainService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainService"/> class.
    /// </summary>
    /// <param name="logger">An instance of <see cref="ILogger"/> used for logging.</param>
    /// <param name="objectMapper">An instance of <see cref="IObjectMapper"/> used for object mapping.</param>
    protected DomainService(ILogger logger, IObjectMapper objectMapper)
        : base(logger, objectMapper) { }
}
